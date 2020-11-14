using DAO.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.KhachHang
{
   public class KhachHangModel
    {
        private static db dbContext = new db();
        public IQueryable<tour_khachhang> getAllKhachHang()
        {
            var khs = dbContext.tour_khachhang;
            return khs;
        }
        public bool addKhachHang(tour_khachhang tour_khachhang)
        {
            bool flag = false;
            try
            {
                dbContext.tour_khachhang.Add(tour_khachhang);
                dbContext.SaveChanges();
                flag = true;
            }
            catch (Exception e)
            {

            }
            return flag;
        }
        public bool editKhachHang(tour_khachhang tour_khachhang)
        {
            bool flag = false;
            try
            {

                var t = dbContext.tour_khachhang.Where(m => m.kh_id == tour_khachhang.kh_id).ToList();
                foreach (tour_khachhang d in t)
                {
                    d.kh_ten = tour_khachhang.kh_ten;
                    d.kh_sdt = tour_khachhang.kh_sdt;
                    d.kh_ngaysinh = tour_khachhang.kh_ngaysinh;
                    d.kh_email = tour_khachhang.kh_email;
                    d.kh_cmnd = tour_khachhang.kh_cmnd;
                }
                flag = true;
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return flag;
        }

        public tour_khachhang getById(string idKhachHang)
        {
            var id = Convert.ToInt32(idKhachHang);
            return dbContext.tour_khachhang.Where(m=>m.kh_id ==id).FirstOrDefault();
        }
    }
}
