namespace tour_du_lich.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_chiphi
    {
        [Key]
        [Column(Order = 0)]
        public int chiphi_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int doan_id { get; set; }

        public decimal chiphi_total { get; set; }

        [Required]
        public string chiphi_chitiet { get; set; }
    }
}
