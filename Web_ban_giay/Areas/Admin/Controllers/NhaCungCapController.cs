using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ban_giay.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        // GET: Admin/NhaCungCap
        public ActionResult Index()
        {
            List<NHACUNGCAP> ncc = db.NHACUNGCAPs.ToList();
            return View(ncc);
        }
        public ActionResult themNCC()
        {
            return View();
        }
       
    }
}