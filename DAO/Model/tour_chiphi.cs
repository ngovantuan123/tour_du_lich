namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_chiphi
    {
        [Key]
        public int chiphi_id { get; set; }

        public int? doan_id { get; set; }

        public decimal? chiphi_total { get; set; }

        public string chiphi_chitiet { get; set; }
    }
}
