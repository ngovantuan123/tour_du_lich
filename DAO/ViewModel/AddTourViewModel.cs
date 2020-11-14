using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class AddTourViewModel
    {
        public String tenTour { get; set; }
        public int loaiTour { get; set; }
        public String motaTour { get; set; }
        public List<int> cacDiemDen { get; set; }

    }
}
