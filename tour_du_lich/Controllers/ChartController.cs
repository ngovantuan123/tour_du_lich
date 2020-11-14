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
        // Variable
        db _context = new db();

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getChart1()
        {
            return PartialView("DoanhThuTourView");
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
                main_model.giaTour =Convert.ToString( t.gia.gia_sotien);
                main_model.soKhach = t.listKH;
                main_model.tenDoan = t.doanKhach.doan_name;
                //main_model.doanhthuDoan =Convert.ToString( main_model.soKhach * t.gia.gia_sotien);
               // main_model.laiDoan = Convert.ToDecimal( main_model.doanhthuDoan) - Convert.ToDecimal(main_model.tongchiphiDoan);
                //main_model.tongChiPhiTour = t.
                list_doanhthu.Add(main_model);               
            });
            decimal tongDoanhthu = 0;
            decimal tongChiPhi = 0;
            decimal tonglai = 0;
           



            return View(list_doanhthu);
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


