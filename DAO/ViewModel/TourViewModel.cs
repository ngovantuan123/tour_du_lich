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
        public List<tour_diadiem> list_diadiem { get; set; }
        public List<String> list_thanhpho { get; set; }

    }
}
