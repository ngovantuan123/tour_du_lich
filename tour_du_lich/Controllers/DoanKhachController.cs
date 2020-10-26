using DAO.DoanKhach;
using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tour_du_lich.Controllers
{
    public class DoanKhachController : Controller
    {
        DoanKhachDAO doanKhachDAO = new DoanKhachDAO();
        // GET: DoanKhach
        public ActionResult Index()
        {
           List<tour_doan> tour_Doans =  doanKhachDAO.findAllDoan();
            return View();
        }
    }
}