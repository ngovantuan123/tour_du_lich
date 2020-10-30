using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DoanKhach
{
    public class DoanKhachModel
    {
        private static db dbContext = new db();

        public IQueryable<DoanKhachViewModel> findAll()
        {
            var data = (from dk in dbContext.tour_doan
                        join t in dbContext.tours on dk.tour_id equals t.tour_id
                        select new DoanKhachViewModel
                        {
                            doanKhach = dk,
                            tour_doan = t,
                        }

                        ).ToList();
            data.ForEach(m =>
            {
            Dictionary<String, Object> temp = getNhanVienAndKhachHang(m.doanKhach.doan_id);
            m.listKH = ((List<tour_khachhang>)temp["kh"]).Count();
            m.listNV = ((List<tour_nhanvien>)temp["nv"]).Count() ;
            });
            
       
         

            return data.AsQueryable<DoanKhachViewModel>();
        }
        public Dictionary<String,Object> getNhanVienAndKhachHang(int idDoan)
        {
            Dictionary<String,Object> result = new Dictionary<string, object>();
            //get nguoi di
            tour_nguoidi nguoidi = dbContext.tour_nguoidi.Where(i => i.doan_id == idDoan).FirstOrDefault();
            List<tour_nhanvien> listNV = new List<tour_nhanvien>();
            if (nguoidi!=null && nguoidi.nguoidi_dsnhanvien != null)
            {
                String[] ids = nguoidi.nguoidi_dsnhanvien.Split(',');
                if (ids.Length != 0)
                {
                    
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = Convert.ToInt32(ids[i]);
                        tour_nhanvien t = dbContext.tour_nhanvien.Where(n => n.nv_id == id).FirstOrDefault();
                        if (t != null)
                        {
                            listNV.Add(t);
                        }
                    }
                   
                }
            }
            result.Add("nv", listNV);
            //get kh
            List<tour_khachhang> listkh = new List<tour_khachhang>();
            if (nguoidi != null && nguoidi.nguoidi_dskhach != null)
            {
                String[] idskh = nguoidi.nguoidi_dskhach.Split(',');
                if (idskh.Length != 0)
                {
                    
                    for (int i = 0; i < idskh.Length; i++)
                    {
                        int id = Convert.ToInt32(idskh[i]);
                        tour_khachhang t = dbContext.tour_khachhang.Where(n => n.kh_id == id).FirstOrDefault();
                        if (t != null)
                        {
                            listkh.Add(t);
                        }
                    }
                    
                }
            }
            result.Add("kh", listkh);
            return result;
        }
        public static List<tour> getAllTour()
        {
            return dbContext.tours.ToList();
        }
        public tour_doan add(tour_doan doan)
        {
            var t = dbContext.tour_doan.Add(doan);
            dbContext.SaveChanges();
            return t;
        }
        public void edit(tour_doan doan)
        {
            var t = dbContext.tour_doan.Where(m => m.doan_id == doan.doan_id).ToList();
            foreach (tour_doan d in t)
            {
                d.doan_name = doan.doan_name;
                d.doan_ngaydi = doan.doan_ngaydi;
                d.doan_ngayve = doan.doan_ngayve;
                d.doan_chitietchuongtrinh = doan.doan_chitietchuongtrinh;
                d.tour_id = doan.tour_id;
            }
            dbContext.SaveChanges();
        }

        public tour_doan getById(int id)
        {
            return dbContext.tour_doan.Where(m => m.doan_id == id).FirstOrDefault();

        }

        public void addKhachHangVaoDoanKhach(DoanKhachViewModel doanKhachViewModel)
        {

        }
    }
}
