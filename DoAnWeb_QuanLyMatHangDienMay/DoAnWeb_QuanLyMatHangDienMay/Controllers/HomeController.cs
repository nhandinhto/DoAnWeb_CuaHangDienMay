using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb_QuanLyMatHangDienMay.Models;

namespace DoAnWeb_QuanLyMatHangDienMay.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        DataClasses1DataContext data = new DataClasses1DataContext("Data Source=DESKTOP-T32VP39\\SQL2012;Initial Catalog=DOANWEB_QLCUAHANGDIENMAY;Integrated Security=True");

        public ActionResult Index() //Giao diện trang chủ
        {
            List<SANPHAM> dsSP = data.SANPHAMs.ToList();
            return View(dsSP);
        }        

        public ActionResult DSLoaiThietBi() //Partial View hiển thị loại thiết bị
        {
            List<LOAITHIETBI> dsSP = data.LOAITHIETBIs.ToList();
            return PartialView(dsSP);
        }

        public ActionResult DSNhaSanXuat()  //Partial View hiển thị nhà sản xuất
        {
            List<NHASANXUAT> dsSP = data.NHASANXUATs.ToList();
            return PartialView(dsSP);
        }

        public ActionResult HTSPTheoLTB(string id)  //Hiển thị danh sách sản phẩm theo loại thiết bị
        {
            List<SANPHAM> dsSP = data.SANPHAMs.Where(t => t.MATHIETBI == id).ToList();
            return View("Index",dsSP);
        }

        public ActionResult HTSPTheoNSX(string id)  //Hiển thị danh sách sản phẩm theo nhà sản xuất
        {
            List<SANPHAM> dsSP = data.SANPHAMs.Where(t => t.MANSX == id).ToList();
            return View("Index", dsSP);
        }

        public ActionResult TimKiem(FormCollection col)  //Hiển thị danh sách tìm kiếm
        {
            string ten = col["txtSearch"];
            List<SANPHAM> dsSP = data.SANPHAMs.Where(t => t.TENSP.Contains(ten)).ToList();
            return View("Index", dsSP);
        }

        public ActionResult HienThiTen()  //Hiển thị tên khách hàng
        {
            ViewBag.kh = Session["kh"];
            return PartialView();
        }

        public ActionResult Index1() //Giao diện trang chủ của quản lý
        {
            List<SANPHAM> dsSP = data.SANPHAMs.ToList();
            return View(dsSP);
        }

        public ActionResult ChiTiet(string id)  //Hiển thị chi tiết
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(t => t.MASP == id);
            return View(sp);
        }

        //public ActionResult TinTuc()
        //{
        //    List<TINTUC> dstt = data.TINTUCs.ToList();
        //    return PartialView(dstt);
        //}

        //public ActionResult ChiTietTinTuc(int id)
        //{
        //    TINTUC tt = data.TINTUCs.SingleOrDefault(t => t.MATIN == id);
        //    return View(tt);
        //}
    }
}
