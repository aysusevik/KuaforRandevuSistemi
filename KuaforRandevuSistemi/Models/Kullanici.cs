namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Gider = new HashSet<Gider>();
            Izin = new HashSet<Izin>();
            Randevu = new HashSet<Randevu>();
        }

        public int kullaniciID { get; set; }

        public int? yetkiID { get; set; }

        public int? firmaID { get; set; }

        [StringLength(30)]
        public string ad { get; set; }

        [StringLength(30)]
        public string soyad { get; set; }

        [StringLength(11)]
        public string telefonNo { get; set; }

        [StringLength(50)]
        public string eposta { get; set; }

        [StringLength(20)]
        public string parola { get; set; }

        [StringLength(150)]
        public string adres { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? iseBaslamaTarihi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? cikisTarihi { get; set; }

        [Column(TypeName = "money")]
        public decimal? maas { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        public virtual Firma Firma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gider> Gider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Izin> Izin { get; set; }

        public virtual Yetki Yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Randevu> Randevu { get; set; }
    }
}
