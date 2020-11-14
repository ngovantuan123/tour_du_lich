using DAO.Model;
using DAO.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DAO.ViewModel;
using System.Text;

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
       
      
        public ActionResult add()
        {
            var data = new TourViewModel();
            data.list_diadiem = tour.getlistDiadiem();
            data.list_thanhpho = tour.getlistThanhpho();
            return View(data);
        }
        [HttpPost]
        public ActionResult Add_new(AddTourViewModel model)
        {
            // tour
            tour t = new tour();
            t.loai_id = model.loaiTour;
            t.tour_ten = model.tenTour;
            t.tour_mota = model.motaTour;
            // add-> save

            _context.tours.Add(t);

           _context.SaveChanges();

            // do save change nên n cập nhật được id mới lun 
            int tour_id = t.tour_id;

            // chi tiet tour
            tour_chitiet chitiet = new tour_chitiet();
            for(int i=0;i< model.cacDiemDen.Count;++i )
            {
                chitiet.tour_id = tour_id;
                chitiet.dd_id = model.cacDiemDen[i];
                chitiet.ct_thutu = i + 1;

                //chitiet.
                // add-> save
                _context.tour_chitiet.Add(chitiet);

                _context.SaveChanges();
            }

            return Redirect("Index"); 
        }
        
        public ActionResult LoadDiadiem(String thanhpho)
        {
            var query = tour.getlistDiadiemByThanhPho(thanhpho);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadCbxLoaiTour()
        {
            var query = tour.getlistLoaiTour();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        // load cbx thành phố
        public ActionResult loadCbxThanhPho()
        {
            var query = tour.getlistThanhpho();
            return Json(query, JsonRequestBehavior.AllowGet);
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
                StringBuilder sqlObject = new StringBuilder();
               
                sqlObject.Append("	with tmp as(	");
                sqlObject.Append("	select t.tour_id,t.tour_ten ,t.tour_mota ,l.loai_ten ,dd.dd_ten");
                sqlObject.Append("	from tours t , tour_chitiet ct, tour_diadiem dd , tour_loai l	");
                sqlObject.Append("	where t.loai_id = l.loai_id	");
                sqlObject.Append("	and t.tour_id = ct.tour_id	");
                sqlObject.Append("	and l.loai_id = t.loai_id	");
                sqlObject.Append("	and dd.dd_id = ct.dd_id	)");
              
              
                sqlObject.Append("	select distinct tour_id as id_tour, tour_ten as ten_tour,tour_mota as mota_tour,loai_ten as loai_tour , STRING_AGG(dd_ten,'-') as diadiem_tour	");
                sqlObject.Append("	from tmp		");
                sqlObject.Append("		group by tour_id,tour_ten,tour_mota,loai_ten		");
    
                var data = _context.Database.SqlQuery<TourViewModel>(sqlObject.ToString()).AsQueryable<TourViewModel>();
                

                //var data = tour.getAllTour();



                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    data = data.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.ten_tour.Contains(searchValue));



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