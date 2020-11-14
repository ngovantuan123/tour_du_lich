using DAO.KhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DAO.Model;

namespace tour_du_lich.Controllers
{
    public class KhachHangController : Controller
    {
        KhachHangModel KhachHangModel = new KhachHangModel();
        // GET: KhachHang
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




                var data = KhachHangModel.getAllKhachHang();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    data = data.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.kh_ten.Contains(searchValue));
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
        public ActionResult addKhachHang()
        {
            tour_khachhang kh = new tour_khachhang();
            return View(kh);
        }
        [HttpPost]
        public ActionResult addKhachHang(tour_khachhang tour_Khachhang)
        {
            try
            {
                KhachHangModel.addKhachHang(tour_Khachhang);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(tour_Khachhang);
            }
        }
        public ActionResult editKhachHang(String idKhachHang)
        {

            tour_khachhang kh = KhachHangModel.getById(idKhachHang);
            return View(kh);
        }
        [HttpPost]
        public ActionResult editKhachHang(tour_khachhang tour_Khachhang)
        {
            try
            {
                KhachHangModel.editKhachHang(tour_Khachhang);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(tour_Khachhang);
            }
        }
    }
}