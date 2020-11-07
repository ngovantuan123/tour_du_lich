namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_doan
    {
        [Key]
        public int doan_id { get; set; }

        public int? tour_id { get; set; }

        public string doan_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? doan_ngaydi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? doan_ngayve { get; set; }

        public string doan_chitietchuongtrinh { get; set; }

        public int? doan_gia_id { get; set; }
    }
}
