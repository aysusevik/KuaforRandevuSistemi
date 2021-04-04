namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hizmet")]
    public partial class Hizmet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hizmet()
        {
            RandevuHizmet = new HashSet<RandevuHizmet>();
        }

        public int hizmetID { get; set; }

        [StringLength(10)]
        public string ad { get; set; }

        public int? sure { get; set; }

        [Column(TypeName = "money")]
        public decimal? ucret { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RandevuHizmet> RandevuHizmet { get; set; }
    }
}
