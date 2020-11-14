using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
	public class DoanhThuTourViewModel
	{
		public int id { get; set; }
		public String ten { get; set; }
		public decimal tongtien { get; set; }
		public decimal chiphi { get; set; }
		public decimal lai { get; set; }
		public DateTime tn { get; set; }
		public DateTime dn { get; set; }
	}
}
