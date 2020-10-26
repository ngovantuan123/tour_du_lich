using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DoanKhach
{
    public class DoanKhachDAO
    {
        public db dbContext = new db();
        public List<tour_doan> findAllDoan()
        {
            return dbContext.tour_doan.ToList();
        }
        
    }
}
