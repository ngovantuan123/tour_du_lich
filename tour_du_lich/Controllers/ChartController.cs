using DAO.DoanKhach;
using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace tour_du_lich.Controllers
{
    public class ChartController : Controller
    {
        db dbContext = new db();
        // Variable
        db _context = new db();
        DoanKhachModel doanKhachModel = new DoanKhachModel();

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getChart1()
        {
            return PartialView("getChart1");
        }


        public ActionResult getThongkeDoanhthuTour([Bind(Include = "tour_id")] tour_gia tour_gia)
        {

            // Tạo cbx
            // Lấy toàn bộ thể loại:
            List<tour> cate = _context.tours.ToList();

            // Tạo SelectList
            SelectList List = new SelectList(cate, "tour_id", "tour_ten", 1);

            // Set vào ViewBag
            ViewBag.tourList = List;


            //
            DoanKhachModel model = new DoanKhachModel();
            var data = model.findAll();
           List<DoanKhachViewModel> _data=data.Where(m => m.doanKhach.tour_id == tour_gia.tour_id).ToList();

            List<doanhThuViewModel> list_doanhthu = new List<doanhThuViewModel>();
            _data.ForEach(t =>
            {
                doanhThuViewModel main_model = new doanhThuViewModel();
                main_model.idDoan = t.doanKhach.doan_id;
                main_model.giaTour =Convert.ToString( t.gia.gia_sotien);
                main_model.soKhach = t.listKH;
                main_model.tenDoan = t.doanKhach.doan_name;
                main_model.tongchiphiDoan = dbContext.tour_chiphi.Where(m=>m.doan_id == t.doanKhach.doan_id).FirstOrDefault().chiphi_total.ToString();
                //main_model.doanhthuDoan =Convert.ToString( main_model.soKhach * t.gia.gia_sotien);
                // main_model.laiDoan = Convert.ToDecimal( main_model.doanhthuDoan) - Convert.ToDecimal(main_model.tongchiphiDoan);

                list_doanhthu.Add(main_model);               
            });
            
           



            return View(list_doanhthu);
        }
        public ActionResult nhanvienditour([Bind(Include = "nv_id")] tour_nhanvien tour_nhanvien,string tn,string dn)
        {
            int nvid = tour_nhanvien.nv_id;
            if(nvid == 0)
            {
                nvid = 1;
            }
            NhanVienDiTourViewModel nvdt = new NhanVienDiTourViewModel();
            List<tour_nhanvien> cate = dbContext.tour_nhanvien.ToList();
            SelectList List = new SelectList(cate, "nv_id", "nv_ten", 1);
            ViewBag.nvList = List;
            string sql;
            if ((tn ==null || dn ==null)||(tn.Equals("") || dn.Equals("")))
            {
                 sql = "declare @tn date = '2020-10-5'\n" +
            "declare @dn date = '2020-12-11' select * from dbo.fn_dsdoan(@tn, @dn)";
            }
            else
            {
                 sql = "declare @tn date = '" + tn + "'\n" +
            "declare @dn date = '" + dn + "' select * from dbo.fn_dsdoan(@tn, @dn)";
            }
            
            
            List<NhanVienDiTourViewModel> temp = dbContext.Database.SqlQuery<NhanVienDiTourViewModel>(sql).ToList();
            
            List<tour> lt = new List<tour>();
            for(int i = 0; i < temp.Count(); i++)
            {
                int idd = temp[i].doan_id;
                Dictionary<String, Object> result = doanKhachModel.getNhanVienAndKhachHang(idd);
                List < tour_nhanvien > nvs = ((List<tour_nhanvien>)result["nv"]);
                nvs.ForEach(m => { 
                    if(nvid == Convert.ToInt32(m.nv_id))
                    {
                        tour_doan d = dbContext.tour_doan.Where(t => t.doan_id == idd).FirstOrDefault();
                        lt.Add(dbContext.tours.Where(t => t.tour_id == d.tour_id).FirstOrDefault());
                    }
                });
                
                
            }
            nvdt.list_t = lt;

            Dictionary<String, int> kq = new Dictionary<string, int>();
            lt.ForEach(m =>
            {
                if (kq.ContainsKey(m.tour_ten))
                {
                    kq[m.tour_ten] += 1;
                }
                else
                {
                    kq.Add(m.tour_ten, 1);
                }
            });
            nvdt.kq = kq;

            return View(nvdt);
        }

        // Chi phi theo tour
        //public ActionResult getDataChiPhi()
        // {
        //     var query = _context.Database.SqlQuery<ChiPhiTourViewModel>(
        //          "select t.tour_ten as tentour, cast(c.chiphi_total as int) as chiphi " +
        //           "from tour_chiphi c, tour_doan d, tours t " +
        //           "where c.doan_id = d.doan_id and d.tour_id = t.tour_id").ToList();
        //     return Json(query, JsonRequestBehavior.AllowGet);

        //   }

        // Thong ke Doanh thu tour
        public ActionResult getDataDoanhThuTour(String tn, String dn)
        {
            String sql = "select* from dbo.fn_doanhthu('" + tn + "', '" + dn + "')"; // ('2020-11-05', '2020-11-11')
            var query = _context.Database.SqlQuery<DoanhThuTourViewModel>(sql).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
    }

}


