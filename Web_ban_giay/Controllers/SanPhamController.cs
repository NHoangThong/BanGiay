using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ban_giay.Controllers
{
    public class SanPhamController : Controller
    {
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        // GET: SanPham
        public ActionResult DanhSachSP()
        {
            var sp = db.SANPHAMs.ToList();
            return PartialView(sp);
        }
        public ActionResult AllSP()
        {
            var sp = db.SANPHAMs.ToList();
            return PartialView(sp);
        }
        public ActionResult CT_SanPham(string maSP)
        {
            var chiTiet = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (chiTiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chiTiet);
        }
    }
}