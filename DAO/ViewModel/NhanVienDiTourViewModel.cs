using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
   public class NhanVienDiTourViewModel
    {
        public List<tour_nhanvien> list_nv { get; set; }
        public List<tour> list_t { get; set; }
        public tour_nhanvien nv { get; set; }
        public int doan_id  { get; set; }
        public Dictionary<String ,int> kq { get; set; }
        
    }
}
