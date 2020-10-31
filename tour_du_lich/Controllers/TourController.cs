using DAO.Model;
using DAO.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DAO.ViewModel;

namespace tour_du_lich.Controllers
{
    public class TourController : Controller
    {

        TourModel tour = new TourModel();

        db _context = new db();

        // GET: Tour
        public ActionResult Index()
        {
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




                // tất cả todo của nhân viên
                var data = tour.getAllTour();



                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    data = data.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.tour.tour_ten.Contains(searchValue));



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

        // chart1
        public ActionResult getDataChart1()
        {
            var query = _context.Database.SqlQuery<Chart1ViewModel>("select l.loai_ten as name, count(t.loai_id) as count from tours t join tour_loai l on t.loai_id = l.loai_id group by l.loai_ten").ToList();
            

            return Json(query, JsonRequestBehavior.AllowGet);

        }
    }
}