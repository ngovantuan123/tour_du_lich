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
                doanKhachViewModel.listTour = DoanKhachModel.getAllTour();
                return View(doanKhachViewModel);
            }
        }
        public ActionResult edit(int id)
        {
            tour_doan doan =  doanKhachModel.getById(id);
            DoanKhachViewModel doanKhachViewModel = new DoanKhachViewModel();
            doanKhachViewModel.doanKhach = doan;
            doanKhachViewModel.listTour = DoanKhachModel.getAllTour();
            return View(doanKhachViewModel);
        }
        [HttpPost]
        public ActionResult edit(DoanKhachViewModel doanKhachViewModel , FormCollection fc)
        {
            var tourId = fc["tour"];
            doanKhachViewModel.doanKhach.tour_id = Convert.ToInt32(tourId);
            try
            {
                doanKhachModel.edit(doanKhachViewModel.doanKhach);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(doanKhachViewModel);
            }
        }
        public ActionResult detail(int id)
        {
           Dictionary<String, Object> kh_nv = doanKhachModel.getNhanVienAndKhachHang(id);
           DoanKhachViewModel doanKhachViewModel = new DoanKhachViewModel();
           doanKhachViewModel.doanKhach = doanKhachModel.getById(id);
           doanKhachViewModel.khachHangs = ((List < tour_khachhang >) kh_nv["kh"]);
           doanKhachViewModel.nhanViens = ((List < tour_nhanvien >) kh_nv["nv"]);
            doanKhachViewModel.nhanVienChuaCoTrongDoan = doanKhachModel.getNhanVienChuaCoTrongDoanKhach(id);
           return View(doanKhachViewModel);
        }
        public ActionResult loadDataTableDSKH()
        {
            var idTour = Request.QueryString["id"];
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

                 Dictionary<String, Object> kh_nv = doanKhachModel.getNhanVienAndKhachHang(Convert.ToInt32(idTour));
                 var data=((List<tour_khachhang>)kh_nv["kh"]).AsQueryable();
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
        }
        public ActionResult loadDataTableDSNV()
        {
            var idTour = Request.QueryString["id"];
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

                Dictionary<String, Object> kh_nv = doanKhachModel.getNhanVienAndKhachHang(Convert.ToInt32(idTour));
                var data = ((List<tour_nhanvien>)kh_nv["nv"]).AsQueryable();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    data = data.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.nv_ten.Contains(searchValue));
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
        }
        [HttpPost]
        public ActionResult themKhachHangVoDoanKhach(DoanKhachViewModel doanKhachViewModel)
        {
            doanKhachModel.addKhachHangVaoDoanKhach(doanKhachViewModel);
           return Redirect("detail?id="+doanKhachViewModel.doanKhach.doan_id);
        }
        [HttpPost]
        public JsonResult themNhanVienVoDoanKhach(String idNv,String idDoan)
        {
            try
            {
                doanKhachModel.themNhanVienVoDoanKhach(idNv, idDoan);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }catch(Exception e)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}