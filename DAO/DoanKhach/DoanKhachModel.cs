using DAO.Model;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
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
                m.listNV = ((List<tour_nhanvien>)temp["nv"]).Count();
                m.gia = dbContext.tour_gia.Where(a => a.gia_id == m.doanKhach.doan_gia_id).FirstOrDefault();
            });




            return data.AsQueryable<DoanKhachViewModel>();
        }
        public Dictionary<String, Object> getNhanVienAndKhachHang(int idDoan)
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();
            //get nguoi di
            tour_nguoidi nguoidi = dbContext.tour_nguoidi.Where(i => i.doan_id == idDoan).FirstOrDefault();
            List<tour_nhanvien> listNV = new List<tour_nhanvien>();
            if (nguoidi != null && nguoidi.nguoidi_dsnhanvien != null && nguoidi.nguoidi_dsnhanvien != " " )
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
            if (nguoidi != null && nguoidi.nguoidi_dskhach != null &&  nguoidi.nguoidi_dskhach != " ")
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
            try { 
            var t = dbContext.tour_doan.Add(doan);
            dbContext.SaveChanges();
            tour_nguoidi nd = new tour_nguoidi();
            
            nd.doan_id = t.doan_id;
            nd.nguoidi_dskhach = " ";
            nd.nguoidi_dsnhanvien =" ";
                string strSQL = "INSERT INTO tour_nguoidi ";
                strSQL += " (doan_id,nguoidi_dsnhanvien,nguoidi_dskhach)";
                strSQL += " VALUES";
                strSQL += " (@doan_id,@nguoidi_dsnhanvien,@nguoidi_dskhach)";
                List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(new SqlParameter("@doan_id", nd.doan_id));
                parameterList.Add(new SqlParameter("@nguoidi_dsnhanvien", nd.nguoidi_dsnhanvien));
                parameterList.Add(new SqlParameter("@nguoidi_dskhach", nd.nguoidi_dsnhanvien));
                SqlParameter[] Param = parameterList.ToArray();

                int noOfRowInserted = dbContext.Database.ExecuteSqlCommand(strSQL, Param);
                return t;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
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
                d.doan_gia_id = doan.doan_gia_id;
            }
            dbContext.SaveChanges();
        }

        public tour_doan getById(int id)
        {
            return dbContext.tour_doan.Where(m => m.doan_id == id).FirstOrDefault();

        }

        public void addKhachHangVaoDoanKhach(String idKh,String idDoan)
        {
                int idd = Convert.ToInt32(idDoan);
                var nguoidi = dbContext.tour_nguoidi.Where(m => m.doan_id == idd).ToList();
                foreach (tour_nguoidi nd in nguoidi)
                {
                    if (nd.nguoidi_dskhach.Equals(" "))
                    {
                        nd.nguoidi_dskhach = idKh;
                    }
                    else
                    {
                        nd.nguoidi_dskhach = nd.nguoidi_dskhach + "," + idKh;
                    }
                }
                string strSQL = "UPDATE tour_nguoidi SET ";
                strSQL += " nguoidi_dskhach=@nguoidi_dskhach WHERE nguoidi_id =@nguoidi_id";
                List<SqlParameter> parameterList = new List<SqlParameter>();
                parameterList.Add(new SqlParameter("@nguoidi_dskhach", nguoidi.FirstOrDefault().nguoidi_dskhach));
                parameterList.Add(new SqlParameter("@nguoidi_id", nguoidi.FirstOrDefault().nguoidi_id));

                SqlParameter[] Param = parameterList.ToArray();

                int noOfRowInserted = dbContext.Database.ExecuteSqlCommand(strSQL, Param);
            }
        public List<tour_nhanvien> getNhanVienChuaCoTrongDoanKhach(int idDoan)
        {
            Dictionary<String, Object> temp = getNhanVienAndKhachHang(idDoan);
            List<tour_nhanvien> nvs = ((List<tour_nhanvien>)temp["nv"]);
            List<tour_nhanvien> kq = new List<tour_nhanvien>();
            if (nvs.Count() == 0)
            {

                kq = dbContext.tour_nhanvien.ToList();
            }
            if (nvs.Count > 0)
            {
                kq = dbContext.tour_nhanvien.ToList();
                nvs.ForEach(m =>
                {
                    if (kq.Contains(m))
                    {
                        kq.Remove(m);
                    }
                });
            }
            return kq;
        }
        public List<tour_khachhang> getKhachHangChuaCoTrongDoanKhach(int idDoan)
        {
            Dictionary<String, Object> temp = getNhanVienAndKhachHang(idDoan);
            List<tour_khachhang> khs = ((List<tour_khachhang>)temp["kh"]);
            List<tour_khachhang> kq = new List<tour_khachhang>();
            if (khs.Count() == 0)
            {

                kq = dbContext.tour_khachhang.ToList();
            }
            if (khs.Count > 0)
            {
                kq = dbContext.tour_khachhang.ToList();
                khs.ForEach(m =>
                {
                    if (kq.Contains(m))
                    {
                        kq.Remove(m);
                    }
                });
            }
            return kq;
        }

        public void themNhanVienVoDoanKhach(string idNv, string idDoan)
        {
             int idd =Convert.ToInt32(idDoan);
            var nguoidi = dbContext.tour_nguoidi.Where(m => m.doan_id == idd).ToList();
            foreach (tour_nguoidi nd in nguoidi)
            {
                if (nd.nguoidi_dsnhanvien.Equals(" "))
                {
                    nd.nguoidi_dsnhanvien = idNv;
                }
                else
                {
                    nd.nguoidi_dsnhanvien = nd.nguoidi_dsnhanvien + "," + idNv;
                }
            }
            string strSQL = "UPDATE tour_nguoidi SET ";
            strSQL += " nguoidi_dsnhanvien=@nguoidi_dsnhanvien WHERE nguoidi_id =@nguoidi_id";
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("@nguoidi_dsnhanvien", nguoidi.FirstOrDefault().nguoidi_dsnhanvien));
            parameterList.Add(new SqlParameter("@nguoidi_id", nguoidi.FirstOrDefault().nguoidi_id));

            SqlParameter[] Param = parameterList.ToArray();

            int noOfRowInserted = dbContext.Database.ExecuteSqlCommand(strSQL, Param);
        }

        public List<tour_gia> getGiaTour(string idTour)
        {
            int _idTour = Convert.ToInt32(idTour);
            return dbContext.tour_gia.Where(m => m.tour_id == _idTour).ToList();        
        }
    }
}
