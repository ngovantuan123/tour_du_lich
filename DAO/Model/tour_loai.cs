namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_loai
    {
        [Key]
        public int loai_id { get; set; }

        [Required]
        public string loai_ten { get; set; }

        [Required]
        public string loai_mota { get; set; }
    }
}
