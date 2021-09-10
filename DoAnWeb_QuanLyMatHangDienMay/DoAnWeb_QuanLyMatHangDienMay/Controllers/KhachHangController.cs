using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb_QuanLyMatHangDienMay.Models;

namespace DoAnWeb_QuanLyMatHangDienMay.Controllers
{
    public class KhachHangController : Controller
    {
        //
        // GET: /KhachHang/
        DataClasses1DataContext data = new DataClasses1DataContext("Data Source=DESKTOP-T32VP39\\SQL2012;Initial Catalog=DOANWEB_QLCUAHANGDIENMAY;Integrated Security=True");

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection col)
        {
            string ten = col["txtDN"];
            string mk = col["txtMK"];
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(t => t.MATKHAU == mk && t.TENDN == ten);
            Session["kh"] = null;
            if (kh == null)
            {
                ViewBag.tb = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
            else
            {
                Session["kh"] = kh;
                if (kh.MALOAI == "LTK001")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index1", "Home");
                }
            }
            
        }

        public ActionResult DangXuat()
        {
            Session["kh"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection col)
        {
            try
            {
                string tenKH = col["txtTenKH"].ToString();
                string diaChi = col["txtDiaChi"].ToString();
                string tenDN = col["txtTenDN"].ToString();
                string matKhau = col["txtMatKhau"].ToString();
                string sdt = col["txtSDT"].ToString();
                KHACHHANG a = new KHACHHANG();
                a.TENKH = tenKH;
                a.TENDN = tenDN;
                a.MATKHAU = matKhau;
                a.DIACHI = diaChi;
                a.SDT = sdt;
                a.MALOAI = "LTK001";
                data.KHACHHANGs.InsertOnSubmit(a);
                data.SubmitChanges();
                Session["kh"] = a;
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return RedirectToAction("DangKy", "KhachHang");
            }
        }
    }
}
