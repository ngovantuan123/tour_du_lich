using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
             var cp= dbContext.tour_chiphi.Where(m => m.doan_id == idDoan).FirstOrDefault();
            List<ChiTietChiPhi> list_ctcp = new List<ChiTietChiPhi>();
            if (cp.chiphi_chitiet != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                ChiTietChiPhi[] cps = js.Deserialize<ChiTietChiPhi[]>(cp.chiphi_chitiet);
                if(cps != null)
                {     
                    for (int i = 0; i < cps.Length; i++)
                    {
                        list_ctcp.Add(cps[i]);
                    }
                }
            }
            return list_ctcp;
        }
        public static void addChiPhi(int idDoan, decimal tongTien,String chiTiet)
        {
            var cp = dbContext.tour_chiphi.Where(m=>m.doan_id == idDoan).ToList();
            foreach (tour_chiphi d in cp)
            {
                d.chiphi_total = tongTien;
                d.chiphi_chitiet = chiTiet;              
            }
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
                ct=dbContext.tour_chiphi.Add(new_ct);
                dbContext.SaveChanges();              
            }
            return ct;
        }
    }
}
