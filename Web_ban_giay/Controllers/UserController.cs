using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ban_giay.Controllers
{
    public class UserController : Controller
    {
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        // GET: User
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult DangKy(NGUOIDUNG nd)
        {
            try
            {
                Session["userReg"] = nd;
                // Mã tự động
                nd.MANGUOIDUNG = GenerateMaNguoiDung();
                // Mã quyền mặc định là 2
                nd.MAQUYEN = 2;

                
                db.NGUOIDUNGs.Add(nd);
              
                db.SaveChanges();

                ViewBag.RegOk = "Đăng kí thành công. Đăng nhập ngay";
                ViewBag.isReg = true;
                return RedirectToAction("DangNhap");
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
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(NGUOIDUNG nd)
        {

            string mail = nd.EMAIL;
            string password=nd.MATKHAU;
            // Hàm kiểm tra tài khoản trong cơ sở dữ liệu
            NGUOIDUNG user = AuthenticateUser(mail, password);

            if (user != null)
            {
                // Đăng nhập thành công
                Session["UserID"] = user.MANGUOIDUNG;
                Session["UserRole"] = user.MAQUYEN;

                if (user.MAQUYEN == 1)
                {
                    Session["use"] = user;
                    // Nếu là admin, chuyển hướng đến trang admin
                    return RedirectToAction("Index", "Admin/HomeAdmin");
                }
                else if (user.MAQUYEN == 2)
                {
                    Session["use"] = user;
                    // Nếu là user, chuyển hướng đến trang user
                    return RedirectToAction("Index", "Home");
                }
            }

           
           
            ViewBag.ErrorMessage = "Đăng nhập thất bại. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.";

            // Thêm biến để hiển thị thông báo khi mật khẩu không đúng
            ViewBag.PasswordIncorrect = true;
            return View();
        }

        private NGUOIDUNG AuthenticateUser(string mail, string password)
        {
            // Thực hiện kiểm tra tài khoản trong cơ sở dữ liệu và trả về thông tin tài khoản
           
            NGUOIDUNG user = db.NGUOIDUNGs.SingleOrDefault(u => u.EMAIL == mail && u.MATKHAU == password);
            return user;
        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult SaveGoogleUserInfo(string name, string email)
        {
            // Thực hiện lưu thông tin người dùng vào CSDL ở đây
            // Sử dụng Entity Framework hoặc truy vấn SQL tùy vào cách bạn đã cấu hình
            // Ví dụ với Entity Framework:

            using (var db = new QL_BAN_GIAY_THETHEOEntities())  // Thay YourDbContext bằng tên của DbContext của bạn
            {
                NGUOIDUNG user = new NGUOIDUNG
                {

                    MANGUOIDUNG = GenerateMaNguoiDung(),
                    TENNGUOIDUNG = name,
                    EMAIL = email,
                    SDT = null,
                    MATKHAU = null,
                    MAQUYEN=2
                    // Các trường thông tin khác của người dùng
                };

                db.NGUOIDUNGs.Add(user);
                db.SaveChanges();
                Session["UserID"] = user.MANGUOIDUNG;
                Session["UserRole"] = user.MAQUYEN;
                Session["use"] = user;
            }

            // Trả về một thông báo thành công hoặc điều hướng đến trang khác
            return Json(new { success = true, message = "Thông tin người dùng đã được lưu." });
        }
    }
}