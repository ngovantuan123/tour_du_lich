namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_diadiem
    {
        [Key]
        public int dd_id { get; set; }

        [Required]
        public string dd_thanhpho { get; set; }

        [Required]
        public string dd_ten { get; set; }

        [Required]
        public string dd_mota { get; set; }
    }
}
