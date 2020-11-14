namespace DAO.Model
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
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal? gia_sotien { get; set; }

        public int? tour_id { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? gia_tungay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? gia_denngay { get; set; }
    }
}
