using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb_QuanLyMatHangDienMay.Models;
using System.IO;

namespace DoAnWeb_QuanLyMatHangDienMay.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        DataClasses1DataContext data = new DataClasses1DataContext();

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

        public ActionResult TinTuc()
        {
            List<TINTUC> dstt = data.TINTUCs.ToList();
            return PartialView(dstt);
        }

        public ActionResult HoTro()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult Add(SANPHAM sp)
        {
            int kq = 1;
            try
            {
                data.SANPHAMs.InsertOnSubmit(sp);
                data.SubmitChanges();
            }
            catch
            {
                kq = 0;
            }
            return Json(kq, JsonRequestBehavior.AllowGet);
        }

        public ActionResult layDsTinTuc()
        {
            TinTucController ttc = new TinTucController();
            List<TINTUC> ds = ttc.GetDSTinTuc();
            return View(ds);
        }

        [HttpGet]
        public ActionResult themMoiTinTuc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult themMoiTinTuc(FormCollection col, HttpPostedFileBase fupload)
        {
            string ten = col["txtTen"].ToString();
            string duong = col["txtDuongDan"].ToString();
            string hinh = fupload.FileName;
            string path = Path.Combine(Server.MapPath("~/Content/HinhTinTuc/" + Path.GetFileName(fupload.FileName)));
            fupload.SaveAs(path);
            TinTucController ttc = new TinTucController();
            TINTUC a = new TINTUC();
            bool kq = ttc.themTinTuc(ten, duong, hinh);
            if(!kq)
            {
                return View();
            }
            return RedirectToAction("layDsTinTuc", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            TinTucController tt = new TinTucController();
            TINTUC a = tt.GetTinTuc(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult CapNhat(FormCollection col, HttpPostedFileBase fupload)
        {
            try
            {
                TinTucController tt = new TinTucController();
                int id = int.Parse(col["txtMa"]);
                string ten = col["txtTen"];
                string duongDan = col["txtDuongDan"];
                string hinh = fupload.FileName;
                fupload.SaveAs(Server.MapPath("~/Content/HinhTinTuc/" + fupload.FileName));
                bool kq = tt.UpdateTinTuc(id, ten, duongDan, hinh);
                if (!kq)
                {
                    return View();
                }
                return RedirectToAction("layDsTinTuc");
            }
            catch
            {
                ViewBag.tb = "Xin vui lòng nhập đủ tất cả thông tin";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            TinTucController tt = new TinTucController();
            tt.XoaTinTuc(id);
            return RedirectToAction("layDsTinTuc", "Home");
        }

        [HttpGet]
        public ActionResult ThemSP()
        {
            return View();
        }
    }
}
