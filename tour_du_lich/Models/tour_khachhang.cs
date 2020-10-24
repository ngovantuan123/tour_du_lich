namespace tour_du_lich.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_khachhang
    {
        [Key]
        public int kh_id { get; set; }

        [Required]
        public string kh_ten { get; set; }

        [Required]
        public string kh_sdt { get; set; }

        [Column(TypeName = "date")]
        public DateTime kh_ngaysinh { get; set; }

        [Required]
        public string kh_email { get; set; }

        [Required]
        public string kh_cmnd { get; set; }
    }
}
