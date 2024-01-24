using CafeOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeOnline.Common;
namespace CafeOnline.Areas.Cashier.Controllers
{
    public class TableStatus
    {
        static CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();

        /// <summary>
        /// Kiểm tra trạng thái bàn
        /// </summary>
        /// <param name="table"></param>
        /// <returns>1: bàn đang có khách</returns>
        /// <returns>2: bàn đã in hóa đơn</returns>
        public static int CheckTable(int table)
        {
            var tb = db.HOADONs.Where(n => n.SoBan == table && n.TrangThai == true
            )
            .FirstOrDefault();
            if (tb == null)
                return -1;
            if (tb.TrangThaiHoaDon == 0)
                return -1;
             if (tb != null && tb.ThoiGianVao > DateTime.Now.AddDays(-1) && tb.ThoiGianRa == CommonConstants.minDateTime && tb.TrangThaiHoaDon != 0 &&tb.TrangThai == true)
                return (int)tb.TrangThaiHoaDon;
            return -1;///Bàn trống
        }


        public static int Get_HoaDonID_by_TableNumber(int table)
        {
            HOADON hd = db.HOADONs.Where(n => n.SoBan == table &&n.TrangThai == true ).FirstOrDefault();
            if (hd == null)
                return -1;
            if (hd.TrangThaiHoaDon == 0 || hd.TrangThai == false)
                return -1;
            //bàn đang có khách là bàn có số hóa đơn trong ngày hôm nay
            if (hd != null && hd.TrangThaiHoaDon != 0 && hd.ThoiGianVao > DateTime.Now.AddDays(-1) && hd.ThoiGianRa == CommonConstants.minDateTime)
                return hd.HoaDonID;

            return -1;//bàn trống
        }


    }
}