namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Izin")]
    public partial class Izin
    {
        public int izinID { get; set; }

        public int? kullaniciID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? baslamaTarihi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? bitisTarihi { get; set; }

        public bool? onayliMi { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
