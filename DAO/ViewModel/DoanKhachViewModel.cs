using DAO.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
   public class DoanKhachViewModel
    {
        public tour_doan doanKhach { get; set; }    
        public tour tour_doan { get; set; }
        public int  listKH { get; set; }
        public List<tour_khachhang> khachHangs { get; set; }
        public List<tour_nhanvien> nhanViens { get; set; }
        public int listNV { get; set; }
        public tour_chiphi chiPhi { get; set; }
        public List<tour> listTour { get; set; }
        public tour_nhanvien nv { get; set; }
        public tour_khachhang kh { get; set; }
        public List<tour_nhanvien> nhanVienChuaCoTrongDoan { get; set; } 

    }
}
