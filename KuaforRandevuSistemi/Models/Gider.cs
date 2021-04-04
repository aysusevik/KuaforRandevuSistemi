namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Gider")]
    public partial class Gider
    {
        public int giderID { get; set; }

        public int? kullaniciID { get; set; }

        public int? turID { get; set; }

        public int? firmaID { get; set; }

        [Column(TypeName = "money")]
        public decimal? tutar { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        [StringLength(500)]
        public string aciklama { get; set; }

        public virtual Firma Firma { get; set; }

        public virtual GiderTur GiderTur { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
