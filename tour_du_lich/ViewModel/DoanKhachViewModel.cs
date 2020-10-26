using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tour_du_lich.ViewModel
{
    public class DoanKhachViewModel
    {
        public List<tour_doan> list_doan { get; set; }
        public List<tour_nhanvien> list_nhanVien { get; set; }
        public List<tour_khachhang> list_KhachHang { get; set; }

    }
}