using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
   public class DoanKhachViewModel
    {
        public tour_doan doanKhach { get; set; }    
        public tour tour_doan { get; set; }
        public List<tour_khachhang> listKH { get; set; }
        public List<tour_nhanvien> listNV { get; set; }
        public tour_chiphi chiPhi { get; set; }

    }
}
