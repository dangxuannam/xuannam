using System.Linq;
using System.Web.Mvc;
using CafeOnline.Models;
namespace CafeOnline.Areas.Cashier.Controllers
{
    public class TongQuanController : BaseCashierController
    {

        CafeOnlineDB db = CafeOnlineDB.ConnectDatabase();


        public ActionResult Index(int ban = 1)
        {
            var lsBan = db.BANs.ToList();
            if (ban > lsBan.Count() || ban <= 0)
                ban = 1;

            ViewBag.Ban = ban;
            int so_hd_ban = TableStatus.Get_HoaDonID_by_TableNumber(ban);
            HOADON hd_ban;
            if (so_hd_ban == -1)
                hd_ban = null;
            else
                hd_ban = db.HOADONs.SingleOrDefault(n => n.HoaDonID == so_hd_ban);
            return View(hd_ban);
        }
    }
}