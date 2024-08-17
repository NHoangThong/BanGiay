using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ban_giay.Models;
namespace Web_ban_giay.Controllers
{
    public class GioHangController : Controller
    {
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        // GET: GioHang

        public List<GioHang> layGioHang()
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if (lstGH == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGH = new List<GioHang>();
                Session["GioHang"] = lstGH;
            }
            return lstGH;
        }
        
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGH = layGioHang();
            ViewBag.tongTien = tongTien();
            return View(lstGH);
        }

        [HttpPost]
        public ActionResult themGioHang(string maSP, string strURL, int quantity)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (sp == null)
            {
                return null;
            }

            // Lấy ra session giỏ hàng
            List<GioHang> ds = layGioHang();

            // Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanPham = ds.Find(n => n.MaSP == maSP);
            if (sanPham == null)
            {
                sanPham = new GioHang(maSP, quantity);
                ds.Add(sanPham);
                return Redirect(strURL);
            }
            else
            {
                sanPham.SoLuong += quantity;
                return Redirect(strURL);
            }
        }

        public ActionResult capNhatGioHang(string maSP, FormCollection f)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (sp == null)
                return null;
            //Lấy ra session giỏ hàng
            List<GioHang> ds = layGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanPham = ds.Find(n => n.MaSP == maSP);
            if(sanPham != null)
            {
                sanPham.SoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult xoaGioHang(string maSP)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (sp == null)
                return null;
            //Lấy ra session giỏ hàng
            List<GioHang> ds = layGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanPham = ds.Find(n => n.MaSP == maSP);
            if (sanPham != null)
            {
                ds.RemoveAll(n => n.MaSP == maSP);

            }
            if(ds.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult suaGioHang()
        {
            if (Session["GioHang"]==null)
                return RedirectToAction("Index", "Home");
            List<GioHang> ds = layGioHang();
            return View(ds);
        }
        private int TongSoLuong()
        {
            int sl = 0;
            List<GioHang> ds = Session["GioHang"] as List<GioHang>;
            if (ds != null)
            {
                sl = ds.Count;
            }
            return sl;
        }
        private double tongTien()
        {
            double s = 0;
            List<GioHang> ds = Session["GioHang"] as List<GioHang>;
            if (ds != null)
            {
                s = ds.Sum(n => n.ThanhTien);
            }
            return s;
        }
        public ActionResult soLuongGH()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }
      
        public ActionResult datHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string maNguoiDung;
            NGUOIDUNG kh = (NGUOIDUNG)Session["use"];
            if (Session["use"] != null)
            {
                maNguoiDung = kh.MANGUOIDUNG;
            }
            else
            {
                return RedirectToAction("DangNhap", "User");
            }
            // Lấy thông tin giỏ hàng từ session
            List<GioHang> ds = layGioHang();

            // Tạo một đơn hàng mới
            DONHANG donHang = new DONHANG
            {
                MADH = "DH" + (db.DONHANGs.Count() + 1).ToString("D2"), // Tạo mã đơn hàng mới
                MANGUOIDUNG = maNguoiDung, // Lấy mã người dùng từ session hoặc đăng nhập
                NGAYDATHANG = DateTime.Now,
                TONGTIEN = ds.Sum(n => n.ThanhTien)
            };

            // Thêm đơn hàng vào CSDL
            db.DONHANGs.Add(donHang);
            db.SaveChanges(); // Lưu thay đổi để có ID của đơn hàng

            // Tạo các chi tiết đơn hàng và thêm vào CSDL
            foreach (var item in ds)
            {
                CHITIETDONHANG chiTietDonHang = new CHITIETDONHANG
                {
                    MACTDH = "CT" + (db.CHITIETDONHANGs.Count() + 1).ToString("D2"), // Tạo mã chi tiết đơn hàng mới
                    MADH = donHang.MADH,
                    MASP = item.MaSP,
                    SOLUONG = item.SoLuong
                };

                // Thêm chi tiết đơn hàng vào CSDL
                db.CHITIETDONHANGs.Add(chiTietDonHang);
                db.SaveChanges();
            }

            // Lưu thay đổi vào CSDL
           // db.SaveChanges();

            // Xóa session giỏ hàng sau khi đặt hàng thành công
            Session["GioHang"] = null;

            return View();
        }
    }
}