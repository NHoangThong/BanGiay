using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ban_giay.Controllers
{
    public class DonHangController : Controller
    {
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        // GET: DonHang
        public ActionResult DS_DonHang()
        {
            if (Session["use"] == null)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("DangNhap", "User"); // Thay "TenController" bằng tên controller quản lý đăng nhập
            }
            NGUOIDUNG kh = (NGUOIDUNG)Session["use"];
            var dh = db.DONHANGs.Where(p=>p.MANGUOIDUNG == kh.MANGUOIDUNG).ToList();
            return PartialView(dh);
        }
        public ActionResult xoaDH(string maDH)
        {
            DONHANG sp = db.DONHANGs.SingleOrDefault(n => n.MADH == maDH);

            if (sp == null)
                return HttpNotFound();
            db.Entry(sp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("DS_DonHang");
        }
    }
}