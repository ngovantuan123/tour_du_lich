using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.NhanVien
{
    public class NhanVienModel
    {
        private static db dbContext = new db();
        public IQueryable<tour_nhanvien> getAllNhanVien()
        {
            var nvs = dbContext.tour_nhanvien;
            return nvs;
        }
        public bool addNhanVien(tour_nhanvien tour_Nhanvien)
        {
            bool flag = false;
            try
            {
                dbContext.tour_nhanvien.Add(tour_Nhanvien);
                dbContext.SaveChanges();
                flag = true;
            }catch(Exception e)
            {
               
            }
            return flag;
        }
        public bool editNhanVien(tour_nhanvien tour_Nhanvien)
        {
            bool flag = false;
            try
            {
                var t = dbContext.tour_nhanvien.Where(m => m.nv_id == tour_Nhanvien.nv_id).ToList();
                foreach (tour_nhanvien d in t)
                {
                    d.nv_ten = tour_Nhanvien.nv_ten;
                    d.nv_sdt = tour_Nhanvien.nv_sdt;
                    d.nv_ngaysinh = tour_Nhanvien.nv_ngaysinh;
                    d.nv_email = tour_Nhanvien.nv_email;
                    d.nv_nhiemvu = tour_Nhanvien.nv_nhiemvu;
                }               
                dbContext.SaveChanges();
                flag = true;
            }
            catch (Exception e)
            {

            }
            return flag;
        }
        public tour_nhanvien getById(String id)
        {
            var idnv = Convert.ToInt32(id);
            return dbContext.tour_nhanvien.Where(m=>m.nv_id == idnv).FirstOrDefault();
        }

    }
}
