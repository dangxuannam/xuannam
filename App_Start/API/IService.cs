using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeOnline.Models;
namespace CafeOnline.API
{
    public interface IService
    {

        int DangNhap(string taikhoan, string matkhau);
        List<MATHANG> Menu();
        int Order(int soban, string maNV, string maMH, int SoLuong, string GhiChu, string maKH);
        NGUOIDUNG XemTaiKhoan(int id);
        
         
    }
}
