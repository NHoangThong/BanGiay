using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_ban_giay.Models
{
    public class GioHang
    {
        QL_BAN_GIAY_THETHEOEntities db = new QL_BAN_GIAY_THETHEOEntities();
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string AnhBia { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien
        {
            get
            {
                return SoLuong * DonGia;
            }
        }
        public GioHang(string masp,int quantity)
        {
            MaSP = masp;
            SANPHAM sp = db.SANPHAMs.Single(n => n.MASP == MaSP);
            TenSP = sp.TENSP;
            AnhBia = sp.HINHANHSP;
            DonGia = double.Parse(sp.GIASP.ToString());
            SoLuong = quantity;

           
        }
    }
}