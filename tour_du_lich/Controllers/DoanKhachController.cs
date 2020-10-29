using DAO.DoanKhach;
using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace tour_du_lich.Controllers
{
    public class DoanKhachController : Controller
    {
        DoanKhachModel doanKhachModel = new DoanKhachModel();
        // GET: DoanKhach
        public ActionResult Index()
        {
           //List<DoanKhachViewModel> tour_Doans = doanKhachModel.findAll();
            return View();
        }
        public ActionResult LoadData()
        {

            try
            {


                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;




                var data = doanKhachModel.findAll();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    data = data.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.doanKhach.doan_name.Contains(searchValue));
                }


                //total number of rows count     
                recordsTotal = data.Count();

                //Sort and Paging     
                var data1 = data.Skip(skip).Take(pageSize).ToList(); // phân trang

                //Returning Json Data  


                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data1, JsonRequestBehavior.AllowGet });

            }

            catch (Exception)
            {
                throw;
            }
            // return View();
        }
        public ActionResult add()
        {
            var data = new DoanKhachViewModel();
            data.listTour = DoanKhachModel.getAllTour();
            return View(data);
        }
        [HttpPost]
        public ActionResult add(DoanKhachViewModel doanKhachViewModel, FormCollection fc)
        {
            var tourId = fc["tour"];
            doanKhachViewModel.doanKhach.tour_id = Convert.ToInt32(tourId);
            try
            {
                tour_doan tour_Doan= doanKhachModel.add(doanKhachViewModel.doanKhach);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(doanKhachViewModel);
            }
        }
        public ActionResult edit()
        {
            return View();
        }
        public ActionResult detail(int id)
        {
            return View();
        }
    }
}