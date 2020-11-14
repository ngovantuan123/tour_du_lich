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
    }
}
