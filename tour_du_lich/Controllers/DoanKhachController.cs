using DAO.DoanKhach;
using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tour_du_lich.Controllers
{
    public class DoanKhachController : Controller
    {
        DoanKhachModel doanKhachModel = new DoanKhachModel();
        // GET: DoanKhach
        public ActionResult Index()
        {
           List<DoanKhachViewModel> tour_Doans = doanKhachModel.findAll();
            return View();
        }
    }
}