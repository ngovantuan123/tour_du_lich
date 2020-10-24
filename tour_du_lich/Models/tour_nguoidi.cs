namespace tour_du_lich.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_nguoidi
    {
        [Key]
        [Column(Order = 0)]
        public int nguoidi_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int doan_id { get; set; }

        [Required]
        public string nguoidi_dsnhanvien { get; set; }

        [Required]
        public string nguoidi_dskhach { get; set; }
    }
}
