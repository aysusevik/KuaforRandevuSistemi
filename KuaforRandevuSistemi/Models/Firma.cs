namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firma")]
    public partial class Firma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Firma()
        {
            Gider = new HashSet<Gider>();
            Kullanici = new HashSet<Kullanici>();
            Randevu = new HashSet<Randevu>();
        }

        public int firmaID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(150)]
        public string adres { get; set; }

        [StringLength(11)]
        public string telefon { get; set; }

        public int? ilceID { get; set; }

        public virtual Ilce Ilce { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gider> Gider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Randevu> Randevu { get; set; }
    }
}
