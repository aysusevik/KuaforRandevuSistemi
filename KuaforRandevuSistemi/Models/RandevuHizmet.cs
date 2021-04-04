namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RandevuHizmet")]
    public partial class RandevuHizmet
    {
        public int randevuHizmetID { get; set; }

        public int randevuID { get; set; }

        public int hizmetID { get; set; }

        [Column(TypeName = "money")]
        public decimal? ucret { get; set; }

        public virtual Hizmet Hizmet { get; set; }

        public virtual Randevu Randevu { get; set; }
    }
}
