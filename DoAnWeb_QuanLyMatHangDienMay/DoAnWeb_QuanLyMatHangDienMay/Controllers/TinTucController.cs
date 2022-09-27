using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DoAnWeb_QuanLyMatHangDienMay.Models;

namespace DoAnWeb_QuanLyMatHangDienMay.Controllers
{
    public class TinTucController : ApiController
    {
        DataClasses1DataContext data = new DataClasses1DataContext();

        [HttpGet]
        public List<TINTUC> GetDSTinTuc()
        {
            return data.TINTUCs.ToList();
        }

        [HttpGet]
        public TINTUC GetTinTuc(int id)
        {
            TINTUC a = data.TINTUCs.SingleOrDefault(t => t.MATIN == id);
            return a;
        }

        [HttpPost]
        public bool themTinTuc(string tenTin, string duongDan, string hinh)
        {
            try
            {
                TINTUC a = new TINTUC();
                a.TENTIN = tenTin;
                a.DUONGDAN = duongDan;
                a.HINH = hinh;
                data.TINTUCs.InsertOnSubmit(a);
                data.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        public bool UpdateTinTuc(int id, string tenTin, string duongDan, string hinh)
        {
            try
            {
                TINTUC a = data.TINTUCs.SingleOrDefault(t => t.MATIN == id);
                if (a != null)
                {
                    a.TENTIN = tenTin;
                    a.DUONGDAN = duongDan;
                    a.HINH = hinh;
                    data.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool XoaTinTuc(int id)
        {
            try
            {
                TINTUC a = data.TINTUCs.SingleOrDefault(t => t.MATIN == id);
                if (a != null)
                {
                    data.TINTUCs.DeleteOnSubmit(a);
                    data.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
