using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Tour
{
    public class TourModel
    {
        private db _context = null;
        public TourModel()
        {
            _context = new db();
        }

        // get List Tour
        public IQueryable<TourViewModel> getAllTour()
        {
        

                var lstdata = (from t in _context.tours
                             join g in _context.tour_gia on t.tour_id equals g.tour_id
                             join dk in _context.tour_doan on t.tour_id equals dk.tour_id
                             join ct in _context.tour_chitiet on t.tour_id equals ct.tour_id
                             join dd  in _context.tour_diadiem on ct.dd_id equals dd.dd_id

                               select new TourViewModel
                             {
                                 tour = t,
                                 gia = g,
                                 doankhach = dk,
                                 diadiem = dd

                             });

           
            return lstdata;

        }
        public List<tour_diadiem> getlistDiadiem()
        {


            var lstdata = _context.tour_diadiem.ToList();

            lstdata = lstdata.Distinct().ToList();


            return lstdata;

        }
        public List<String> getlistThanhpho()
        {


            List<String> s = new List<String>();
            foreach (var item in getlistDiadiem())
            {
                s.Add(item.dd_thanhpho);
            }

          


            return s.Distinct().ToList();

        }
        public List<tour_diadiem> getlistDiadiemByThanhPho(String thanhpho)
        {

            var lstdata = _context.tour_diadiem.Where(d => d.dd_thanhpho == thanhpho).ToList();
               
          




            return lstdata;

        }







    }
}
