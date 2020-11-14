using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class TourViewModel
    {
        
        public tour tour { get; set; }
        public tour_gia gia { get; set; }
        public tour_doan doankhach { get; set; }
        public tour_diadiem diadiem { get; set; }
        public tour_loai loai{ get; set; }
       

        public List<tour_diadiem> list_diadiem { get; set; }
        public List<String> list_thanhpho { get; set; }

        // edited
        public int id_tour { get; set; }
        public String ten_tour { get; set; }
        public String loai_tour { get; set; }
        public String mota_tour { get; set; }
        public String diadiem_tour{ get; set; }



    }
}
