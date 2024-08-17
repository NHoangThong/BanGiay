using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using System.Data.Entity.SqlServer;
namespace Web_ban_giay.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        //public ActionResult Index(int page=1)
        //{

        //    int pageSize = 3;
        //    var sp = db.SANPHAMs.OrderBy(x=>x.MASP).ToPagedList(page, pageSize);
        //    return View(sp);
        //}
        public ActionResult Index(string searchQuery, int page = 1)
        {
            int pageSize = 3;

            // Lấy tất cả sản phẩm từ cơ sở dữ liệu
            var products = db.SANPHAMs.OrderBy(x => x.MASP);

            // Lọc sản phẩm dựa trên query tìm kiếm
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => SqlFunctions.PatIndex("%" + searchQuery + "%", p.TENSP) > 0).OrderBy(x => x.MASP);
            }

            // Tạo danh sách phân trang
            var pagedList = products.ToPagedList(page, pageSize);

            // Truyền query tìm kiếm cho view để hiển thị trong ô nhập tìm kiếm
            ViewBag.SearchQuery = searchQuery;

            return View(pagedList);
        }
        public ActionResult chiTietSP(string maSP)
        {
            var sp  = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            return View(sp);
        }
        public ActionResult themSP()
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MANHACUNGCAP", "TENNHACUNGCAP");
            return View();
        }
     
        [HttpPost]
        public ActionResult themSP(SANPHAM sp, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string _FileName = imageFile.FileName;
                
                string _path = Path.Combine(Server.MapPath("~/assets/image"), _FileName);
                imageFile.SaveAs(_path);

                sp.HINHANHSP = _FileName;

            }
            if (ModelState.IsValid)
            {
              
               
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
            }
           
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MANHACUNGCAP", "TENNHACUNGCAP", sp.MANHACUNGCAP);
            return RedirectToAction("Index");
        }
      
        public ActionResult suaSP(string maSP) 
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);

            if(sp==null)
                return HttpNotFound();
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MANHACUNGCAP", "TENNHACUNGCAP");
            return View(sp);
        }

        [HttpPost]
        public ActionResult suaSP(SANPHAM sp, HttpPostedFileBase imageFile, string existingImage)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                string _FileName = imageFile.FileName;

                string _path = Path.Combine(Server.MapPath("~/assets/image"), _FileName);
                imageFile.SaveAs(_path);

                sp.HINHANHSP = _FileName;

            }
            else
            {
                // Nếu không có hình ảnh mới được chọn, sử dụng tên hình ảnh hiện tại
                sp.HINHANHSP = existingImage;
            }
            if (ModelState.IsValid && !string.IsNullOrEmpty(sp.HINHANHSP))
            {
                db.SANPHAMs.Attach(sp);
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MANHACUNGCAP", "TENNHACUNGCAP", sp.MANHACUNGCAP);
            return View(sp);
        }

        public ActionResult xoaSP(string maSP)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);

            if (sp == null)
                return HttpNotFound();
            string imagePath = Path.Combine(Server.MapPath("~/assets/image"), sp.HINHANHSP);
            db.Entry(sp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            // Xóa tệp tin hình ảnh
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThemNguoiDung()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNguoiDung(NGUOIDUNG nd)
        {
            ViewData["MAQUYEN"] = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Quyền 1" },
                new SelectListItem { Value = "2", Text = "Quyền 2" },
                // Thêm các mục khác nếu cần
            };
            try
            {
                Session["userReg"] = nd;
                // Mã tự động
                nd.MANGUOIDUNG = GenerateMaNguoiDung();
                db.NGUOIDUNGs.Add(nd);
                db.SaveChanges();
                ViewBag.isReg = true;
                return RedirectToAction("DanhSachUser");
            }
            catch
            {
                return View();
            }
        }
        private string GenerateMaNguoiDung()
        {
            // Truy vấn SQL để lấy MANGUOIDUNG lớn nhất
            string sqlQuery = "SELECT MAX(CAST(SUBSTRING(MANGUOIDUNG, 3, LEN(MANGUOIDUNG) - 2) AS INT)) FROM NGUOIDUNG";

            // Lấy giá trị lớn nhất
            int maxId = db.Database.SqlQuery<int?>(sqlQuery).FirstOrDefault() ?? 0;

            // Tăng giá trị lớn nhất để tạo mã mới
            int nextId = maxId + 1;

            // Format lại mã người dùng mới
            //định dạng để đảm bảo rằng nó luôn có ít nhất 2 chữ số.
            return "ND" + nextId.ToString("D2");
        }

        public ActionResult DanhSachUser()
        {
            var us = db.NGUOIDUNGs.ToList();
            return View(us);
        }
        public ActionResult suaUS(string maUS)
        {
            NGUOIDUNG us = db.NGUOIDUNGs.SingleOrDefault(n => n.MANGUOIDUNG == maUS);

            if (us == null)
                return HttpNotFound();
            return View(us);
        }

        [HttpPost]
        public ActionResult suaUS(NGUOIDUNG us)
        {
            if (ModelState.IsValid)
            {
                db.NGUOIDUNGs.Attach(us);
                db.Entry(us).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachUser");
            }
            return View(us);
        }
        public ActionResult xoaUS(string maUS)
        {
            NGUOIDUNG us = db.NGUOIDUNGs.SingleOrDefault(n => n.MANGUOIDUNG == maUS);

            if (us == null)
                return HttpNotFound();
            db.Entry(us).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("DanhSachUser");
        }

        public ActionResult DSDonHangAdmin()
        {
            var dh = db.DONHANGs.ToList();
            return View(dh);
        }
        public ActionResult suaDH(string maDH)
        {
            DONHANG dh = db.DONHANGs.SingleOrDefault(n => n.MADH == maDH);

            if (dh == null)
                return HttpNotFound();
            return View(dh);
        }

        [HttpPost]
        public ActionResult suaDH(DONHANG dh)
        {
            if (ModelState.IsValid)
            {
                db.DONHANGs.Attach(dh);
                db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSDonHangAdmin");
            }
            return View(dh);
        }
        public ActionResult xoaDH(string maDH)
        {
            DONHANG dh = db.DONHANGs.SingleOrDefault(n => n.MADH == maDH);

            if (dh == null)
                return HttpNotFound();
            db.Entry(dh).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("DSDonHangAdmin");
        }
    }
}