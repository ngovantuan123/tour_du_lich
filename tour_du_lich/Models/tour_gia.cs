namespace tour_du_lich.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_gia
    {
        [Key]
        public int gia_id { get; set; }

        public decimal gia_sotien { get; set; }

        public int tour_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime gia_tungay { get; set; }

        [Column(TypeName = "date")]
        public DateTime gia_denngay { get; set; }
    }
}
