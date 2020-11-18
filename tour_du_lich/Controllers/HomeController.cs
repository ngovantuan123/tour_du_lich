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
        public ActionResult tongsodoantrongnam()
        {
            string sql = "select count(doan_id) as sldoan\n" +
                        "from tour_doan d\n" +
                        "where doan_ngaydi >= DATEFROMPARTS(YEAR(GETDATE()), 1, 1)\n" +
                            "and doan_ngayve <= DATEFROMPARTS(YEAR(GETDATE()), 12, 31);";
            var data = _context.Database.SqlQuery<DashBoardViewModel>(sql).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult tourhoatdongnhieunhat()
        {
            String sql = "select tour_ten as tour_di_nhieu_nhat\n"+
                         "from tours t, (select top 1 tour_id, count(doan_id) as sldoan\n" +
                                        "from tour_doan\n" +
                                        "group by tour_id) tmp\n" +
                          "where t.tour_id = tmp.tour_id";
            var data = _context.Database.SqlQuery<DashBoardViewModel>(sql).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult tourdoanhthucaonhat() {
            string sql =
            "select tour_ten as tour_doanh_thu_cao_nhat\n" +
"from(select top 1 t.tour_ten, g.gia_sotien * dbo.COUNT_PEOPLE(d.doan_id) as sotien\n" +
        "from tour_doan d, tours t, tour_gia g\n" +
       " where d.tour_id = t.tour_id and t.tour_id = g.tour_id\n" +
           " and d.doan_ngaydi >= g.gia_tungay\n" +
          "  and d.doan_ngayve <= g.gia_denngay) tmp";
            var data = _context.Database.SqlQuery<DashBoardViewModel>(sql).ToList();
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