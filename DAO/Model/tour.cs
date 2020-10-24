namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour
    {
        [Key]
        public int tour_id { get; set; }

        [Required]
        public string tour_ten { get; set; }

        [Required]
        public string tour_mota { get; set; }

        public int loai_id { get; set; }

        public int gia_id { get; set; }
    }
}
