namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_nhanvien
    {
        [Key]
        public int nv_id { get; set; }

        public string nv_ten { get; set; }

        public string nv_sdt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nv_ngaysinh { get; set; }

        public string nv_email { get; set; }

        public string nv_nhiemvu { get; set; }
    }
}
