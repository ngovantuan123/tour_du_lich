namespace DAO.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tour_chitiet
    {
        [Key]
        public int ct_id { get; set; }

        public int tour_id { get; set; }

        public int dd_id { get; set; }

        public int ct_thutu { get; set; }
    }
}
