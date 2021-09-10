using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb_QuanLyMatHangDienMay.Models
{
    public class CartItem
    {
        public string maSP { get; set; }
        public string tenSP { get; set; }
        public double donGia { get; set; }
        public string hinh { get; set; }
        public int soLuong { get; set; }
        public double thanhTien
        {
            get { return soLuong * donGia; }
        }

        DataClasses1DataContext data = new DataClasses1DataContext();

        //Phương thức khởi tạo giỏ hàng
        public CartItem(string ma)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(t => t.MASP == ma);
            if (sp != null)
            {
                if (sp.GIAMGIA != null)
                {
                    maSP = ma;
                    tenSP = sp.TENSP;
                    donGia = double.Parse(sp.GIAMGIA.ToString());
                    hinh = sp.HINH;
                    soLuong = 1;
                }
                else
                {
                    maSP = ma;
                    tenSP = sp.TENSP;
                    donGia = double.Parse(sp.DONGIA.ToString());
                    hinh = sp.HINH;
                    soLuong = 1;
                }
            }
        }
    }
}