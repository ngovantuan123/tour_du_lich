namespace tour_du_lich.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_loaichiphi
    {
        [Key]
        public int cp_id { get; set; }

        [Required]
        public string cp_ten { get; set; }

        [Required]
        public string cp_mota { get; set; }
    }
}
