using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ChiPhi
{
    public class ChiPhiModel
    {
        private  static db dbContext = new db();
        public static List<tour_loaichiphi> getAllLoaiChiPhi()
        {
            return dbContext.tour_loaichiphi.ToList();
        }
        public static List<ChiTietChiPhi> getChiTiet(int idDoan)
        {
            return null;
        }
        public static void addChiPhi(int idDoan, decimal tongTien,String chiTiet)
        {
            tour_chiphi cp = new tour_chiphi();
            cp.doan_id = idDoan;
            cp.chiphi_total =tongTien;
            cp.chiphi_chitiet = chiTiet;
            dbContext.tour_chiphi.Add(cp);
            dbContext.SaveChanges();
        }
        public static tour_chiphi getChiPhiByDoanId(int idDoan)
        {
            var ct = dbContext.tour_chiphi.Where(m => m.doan_id == idDoan).FirstOrDefault();
            if (ct == null)
            {
                tour_chiphi new_ct = new tour_chiphi();
                new_ct.doan_id = idDoan;
                new_ct.chiphi_total = 0;
                dbContext.tour_chiphi.Add(new_ct);
                dbContext.SaveChanges();
                ct = dbContext.tour_chiphi.Where(m => m.doan_id == idDoan).FirstOrDefault();

            }
            return ct;
        }
    }
}
