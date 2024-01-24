using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeOnline.Models
{
    /// <summary>
    /// sử dụng để xử lý trên form bán hàng
    /// </summary>
    public class ChiTietDatHangTheoBan
    {
        public int SoBan { get; set; }

        public int SoHoaDon { get; set; }

        public decimal TongTien { get; set; }

        public int TongSoLuongMon { get; set; }



        public int MaKH { get; set; }

        /// <summary>
        /// 0: Da thanh toan
        /// 1: Ban trong
        /// 2: Dang co khach
        /// 3: Da in hoa don
        /// -1: không có bàn trên hệ thống
        /// </summary>
        public int TrangThai { get; set; }

    }
}