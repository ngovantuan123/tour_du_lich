using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace tour_du_lich.Controllers
{
    public class HomeController : Controller
    {
        db _context = new db();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getSoTien1nam()
        {
            StringBuilder sqlObject = new StringBuilder();

            
            sqlObject.Append("	select sum(g.gia_sotien * dbo.COUNT_PEOPLE(d.doan_id)) as sotien1nam	");
            sqlObject.Append("	from tour_doan d, tours t, tour_gia g	");
            sqlObject.Append("	where d.tour_id = t.tour_id and t.tour_id = g.tour_id	");
            sqlObject.Append("	    and d.doan_ngaydi >= g.gia_tungay	");
            sqlObject.Append("	    and d.doan_ngayve <= g.gia_denngay	");
            sqlObject.Append("	    and doan_ngaydi >= DATEFROMPARTS(YEAR(GETDATE()), 1, 1)	");
            sqlObject.Append("	    and doan_ngayve <= DATEFROMPARTS(YEAR(GETDATE()), 12, 31)	");

            var data = _context.Database.SqlQuery<DashBoardViewModel>(sqlObject.ToString()).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}