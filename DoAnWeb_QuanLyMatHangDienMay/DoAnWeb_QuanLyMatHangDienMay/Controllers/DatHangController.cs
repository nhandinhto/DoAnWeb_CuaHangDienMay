using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb_QuanLyMatHangDienMay.Models;

namespace DoAnWeb_QuanLyMatHangDienMay.Controllers
{
    public class DatHangController : Controller
    {
        //
        // GET: /DatHang/

        DataClasses1DataContext data = new DataClasses1DataContext();

        public ActionResult XemGioHang()
        {
            GioHang gio = (GioHang)Session["gh"];
            if (gio == null)
                gio = new GioHang();
            return View(gio);
        }

        public ActionResult ThemMatHang(string id)
        {
            GioHang gio = (GioHang)Session["gh"];
            if (gio == null)
                gio = new GioHang();
            bool kq = gio.themSP(id);
            Session["gh"] = gio;
            return RedirectToAction("Index","Home");
        }

        public ActionResult XoaMatHang(string id)
        {
            GioHang gio = (GioHang)Session["gh"];
            CartItem a = gio.dsSP.Find(t => t.maSP == id);
            if (a == null)
            {
                return RedirectToAction("XemGioHang", "DatHang");
            }
            gio.dsSP.Remove(a);
            return RedirectToAction("XemGioHang","DatHang");
        }

        [HttpGet]
        public ActionResult TaoDonDatHang() //Tạo đơn đặt hàng, nếu chưa đặt hàng sẽ đăng nhập
        {
            KHACHHANG kh = (KHACHHANG)Session["kh"];
            if (kh == null)
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            ViewBag.k = kh;
            GioHang gio = (GioHang)Session["gh"];
            if (gio == null)
                gio = new GioHang();
            return View(gio);
        }

        [HttpPost]
        public ActionResult LuuDonDatHang(FormCollection col) //Lưu đơn đặt hàng vào csdl, Thông báo người dùng đặt hành thành công
        {
            DateTime ngayGiao;
            if (!DateTime.TryParse(col["txtNgayGiao"].ToString(), out ngayGiao))
            {
                ViewBag.tb = "Xin vui lòng chọn ngày giao hàng";
                return RedirectToAction("TaoDonDatHang", "DatHang");
            }
            else
            {                
                TimeSpan tt = ngayGiao - DateTime.Now;
                double diff = tt.TotalDays;
                if (diff < 0)
                {
                    ViewBag.tb = "Ngày giao hàng phải lớn hơn ngày hiện tại";
                    return RedirectToAction("TaoDonDatHang", "DatHang");
                }
                KHACHHANG kh = (KHACHHANG)Session["kh"];
                GioHang gio = (GioHang)Session["gh"];
                HOADON hd = new HOADON();
                hd.NGAYHOADON = DateTime.Now;
                hd.NGAYGIAO = ngayGiao;
                hd.MAKH = kh.MAKH;
                hd.TONGTIEN = gio.tongThanhTien();
                data.HOADONs.InsertOnSubmit(hd);
                data.SubmitChanges();

                foreach (CartItem item in gio.dsSP)
                {
                    CHITIETHOADON cthd = new CHITIETHOADON();
                    cthd.MAHD = hd.MAHD;
                    cthd.MASP = item.maSP;
                    cthd.SOLUONG = item.soLuong;
                    cthd.DONGIA = item.donGia;
                    cthd.THANHTIEN = item.thanhTien;
                    data.CHITIETHOADONs.InsertOnSubmit(cthd);
                    data.SubmitChanges();
                }
                gio.dsSP.Clear();
                return View();
            }
        }
    }
}
