namespace DAO.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public partial class db : DbContext
    {
        public db()
            : base("name=db")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tour_chiphi> tour_chiphi { get; set; }
        public virtual DbSet<tour_chitiet> tour_chitiet { get; set; }
        public virtual DbSet<tour_diadiem> tour_diadiem { get; set; }
        public virtual DbSet<tour_doan> tour_doan { get; set; }
        public virtual DbSet<tour_gia> tour_gia { get; set; }
        public virtual DbSet<tour_khachhang> tour_khachhang { get; set; }
        public virtual DbSet<tour_loai> tour_loai { get; set; }
        public virtual DbSet<tour_loaichiphi> tour_loaichiphi { get; set; }
        public virtual DbSet<tour_nguoidi> tour_nguoidi { get; set; }
        public virtual DbSet<tour_nhanvien> tour_nhanvien { get; set; }
        public virtual DbSet<tour> tours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tour_chiphi>()
                .Property(e => e.chiphi_total)
                .HasPrecision(10, 0);

            modelBuilder.Entity<tour_chiphi>()
                .Property(e => e.chiphi_chitiet)
                .IsUnicode(false);

            modelBuilder.Entity<tour_diadiem>()
                .Property(e => e.dd_thanhpho)
                .IsUnicode(false);

            modelBuilder.Entity<tour_diadiem>()
                .Property(e => e.dd_ten)
                .IsUnicode(false);

            modelBuilder.Entity<tour_diadiem>()
                .Property(e => e.dd_mota)
                .IsUnicode(false);

            modelBuilder.Entity<tour_doan>()
                .Property(e => e.doan_name)
                .IsUnicode(false);

            modelBuilder.Entity<tour_doan>()
                .Property(e => e.doan_chitietchuongtrinh)
                .IsUnicode(false);

            modelBuilder.Entity<tour_gia>()
                .Property(e => e.gia_sotien)
                .HasPrecision(10, 0);

            modelBuilder.Entity<tour_khachhang>()
                .Property(e => e.kh_ten)
                .IsUnicode(false);

            modelBuilder.Entity<tour_khachhang>()
                .Property(e => e.kh_sdt)
                .IsUnicode(false);

            modelBuilder.Entity<tour_khachhang>()
                .Property(e => e.kh_email)
                .IsUnicode(false);

            modelBuilder.Entity<tour_khachhang>()
                .Property(e => e.kh_cmnd)
                .IsUnicode(false);

            modelBuilder.Entity<tour_loai>()
                .Property(e => e.loai_ten)
                .IsUnicode(false);

            modelBuilder.Entity<tour_loai>()
                .Property(e => e.loai_mota)
                .IsUnicode(false);

            modelBuilder.Entity<tour_loaichiphi>()
                .Property(e => e.cp_ten)
                .IsUnicode(false);

            modelBuilder.Entity<tour_loaichiphi>()
                .Property(e => e.cp_mota)
                .IsUnicode(false);

            modelBuilder.Entity<tour_nguoidi>()
                .Property(e => e.nguoidi_dsnhanvien)
                .IsUnicode(false);

            modelBuilder.Entity<tour_nguoidi>()
                .Property(e => e.nguoidi_dskhach)
                .IsUnicode(false);

            modelBuilder.Entity<tour_nhanvien>()
                .Property(e => e.nv_ten)
                .IsUnicode(false);

            modelBuilder.Entity<tour_nhanvien>()
                .Property(e => e.nv_sdt)
                .IsUnicode(false);

            modelBuilder.Entity<tour_nhanvien>()
                .Property(e => e.nv_email)
                .IsUnicode(false);

            modelBuilder.Entity<tour_nhanvien>()
                .Property(e => e.nv_nhiemvu)
                .IsUnicode(false);

            modelBuilder.Entity<tour>()
                .Property(e => e.tour_ten)
                .IsUnicode(false);

            modelBuilder.Entity<tour>()
                .Property(e => e.tour_mota)
                .IsUnicode(false);
        }
    }
}
