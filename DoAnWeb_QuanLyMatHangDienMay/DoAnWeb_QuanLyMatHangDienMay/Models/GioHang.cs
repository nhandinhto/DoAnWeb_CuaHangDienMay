using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb_QuanLyMatHangDienMay.Models
{
    public class GioHang
    {
        public List<CartItem> dsSP;

        //Phương thức khởi tạo
        public GioHang()
        {
            dsSP = new List<CartItem>();
        }

        public GioHang(List<CartItem> lst)
        {
            dsSP = lst;
        }

        //Tổng số lượng mặt hàng
        public int tongSoLuongMatHang()
        {
            if (dsSP == null)
                return 0;
            return dsSP.Sum(t => t.soLuong);
        }

        //Đếm số mặt hàng
        public int demSoMatHang()
        {
            if (dsSP == null)
                return 0;
            return dsSP.Count;
        }

        //Tổng thành tiền
        public double tongThanhTien()
        {
            if (dsSP == null) 
                return 0;
            return dsSP.Sum(t => t.thanhTien);
        }
        
        //Thêm sản phẩm vào giỏ
        public bool themSP(string ma)
        {
            CartItem sp = dsSP.Find(t => t.maSP == ma);
            if (sp == null) //Sản phẩm chưa có trong giỏ
            {
                CartItem a = new CartItem(ma);
                if (a == null)
                    return false;
                dsSP.Add(a);
            }
            else
            {
                sp.soLuong++;
            }
            return true;
        }
    }
}