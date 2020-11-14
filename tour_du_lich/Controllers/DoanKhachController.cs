using DAO.DoanKhach;
using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DAO.ChiPhi;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace tour_du_lich.Controllers
{
    public class DoanKhachController : Controller
    {
        DoanKhachModel doanKhachModel = new DoanKhachModel();
        ChiPhiModel ChiPhiModel = new ChiPhiModel();
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
                return null;
            }
            // return View();
        }
        public ActionResult add()
        {
            var data = new DoanKhachViewModel();
            data.listTour = DoanKhachModel.getAllTour();
            data.list_gia =  doanKhachModel.getGiaTour(Convert.ToString(data.listTour.FirstOrDefault().tour_id));
            return View(data);
        }
        [HttpPost]
        public ActionResult add(DoanKhachViewModel doanKhachViewModel, FormCollection fc)
        {
            var tourId = fc["tour"];
            var giaId = fc["gia"];
            doanKhachViewModel.doanKhach.tour_id = Convert.ToInt32(tourId);
            doanKhachViewModel.doanKhach.doan_gia_id = Convert.ToInt32(giaId);
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
            doanKhachViewModel.list_gia = doanKhachModel.getGiaTour(Convert.ToString(doan.doan_gia_id));
            return View(doanKhachViewModel);
        }
        [HttpPost]
        public ActionResult edit(DoanKhachViewModel doanKhachViewModel , FormCollection fc)
        {
            var tourId = fc["tour"];
            var gia_Id = fc["gia"];
            doanKhachViewModel.doanKhach.tour_id = Convert.ToInt32(tourId);
            doanKhachViewModel.doanKhach.doan_gia_id = Convert.ToInt32(gia_Id);
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
           doanKhachViewModel.khachHangChuaCoTrongDoan = doanKhachModel.getKhachHangChuaCoTrongDoanKhach(id);
           doanKhachViewModel.list_loaiCP = ChiPhiModel.getAllLoaiChiPhi();
           doanKhachViewModel.chiPhi = ChiPhiModel.getChiPhiByDoanId(id);
           doanKhachViewModel.list_chitietcp=ChiPhiModel.getChiTiet(id);
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
                return null;
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
                return null;
            }
        }
        public ActionResult loadDataTableDSCP()
        {
            var id = Request.QueryString["idDoan"];
            int idDoan = Convert.ToInt32(id);
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
                var data = ChiPhiModel.getChiTiet(idDoan).AsQueryable();
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    data = data.OrderBy(sortColumn + " " + sortColumnDir);
                }
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(m => m.tenChiPhi.Contains(searchValue));
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
                return null;
            }
        }

        [HttpPost]
        public ActionResult themKhachHangVoDoanKhach(String idKh, String idDoan)
        {
            try
            {
                doanKhachModel.addKhachHangVaoDoanKhach(idKh, idDoan);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
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
        [HttpPost]
        public JsonResult getGiaTour(String idTour)
        {
            try
            {
                List<tour_gia>  list_Gia= doanKhachModel.getGiaTour(idTour);
                return Json(new { Success = true,gias =list_Gia }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult themChiPhi(String idDoan,String tencp,string hoadon,string ngayhoadon,string noidung,string sotien)
        {
           int  idD = Convert.ToInt32(idDoan);
            try
            {
                tour_chiphi cp = ChiPhiModel.getChiPhiByDoanId(idD);
                List<ChiTietChiPhi> list_ctcp = new List<ChiTietChiPhi>();
                if(cp.chiphi_chitiet != null) { 
                JavaScriptSerializer js = new JavaScriptSerializer();
                ChiTietChiPhi[] cps = js.Deserialize<ChiTietChiPhi[]>(cp.chiphi_chitiet);
                if(cps != null) { 
                    for(int i = 0; i < cps.Length; i++)
                        {
                            list_ctcp.Add(cps[i]);
                        }
                }
                }
                ChiTietChiPhi ct = new ChiTietChiPhi();
                ct.loaiChiPhi = tencp;
                ct.tenChiPhi = noidung;
                ct.hoaDon = hoadon;
                ct.ngayHD = ngayhoadon;
                ct.soTien = sotien; 
                list_ctcp.Add(ct);
                var json = JsonConvert.SerializeObject(list_ctcp);    
                               
                decimal tongTien = (decimal)cp.chiphi_total;
                tongTien = tongTien + Convert.ToDecimal(sotien);
                ChiPhiModel.addChiPhi(Convert.ToInt32(idD),tongTien,json.ToString());
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch(Exception e)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
           
           
        }

    }
}