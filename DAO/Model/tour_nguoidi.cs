namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_nguoidi
    {
        [Key]
        public int nguoidi_id { get; set; }

        public int? doan_id { get; set; }

        public string nguoidi_dsnhanvien { get; set; }

        public string nguoidi_dskhach { get; set; }
    }
}
