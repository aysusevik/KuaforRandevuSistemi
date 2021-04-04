namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Randevu")]
    public partial class Randevu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Randevu()
        {
            RandevuHizmet = new HashSet<RandevuHizmet>();
        }

        public int randevuID { get; set; }

        public int? musteriID { get; set; }

        public int? kullaniciID { get; set; }

        public int? firmaID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public TimeSpan? saat { get; set; }

        public bool? geldiMi { get; set; }

        [StringLength(500)]
        public string degerlendirme { get; set; }

        public bool? onayliMi { get; set; }

        public virtual Firma Firma { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Musteri Musteri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RandevuHizmet> RandevuHizmet { get; set; }
    }
}
