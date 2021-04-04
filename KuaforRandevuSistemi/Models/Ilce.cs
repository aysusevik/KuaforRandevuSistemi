namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ilce")]
    public partial class Ilce
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ilce()
        {
            Firma = new HashSet<Firma>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ilceID { get; set; }

        public int ilID { get; set; }

        [Required]
        [StringLength(60)]
        public string ilceAdi { get; set; }

        [Required]
        [StringLength(55)]
        public string ilAdi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Firma> Firma { get; set; }

        public virtual Il Il { get; set; }
    }
}
