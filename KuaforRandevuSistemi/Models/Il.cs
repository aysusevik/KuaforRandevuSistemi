namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Il")]
    public partial class Il
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Il()
        {
            Ilce = new HashSet<Ilce>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ilID { get; set; }

        [Required]
        [StringLength(20)]
        public string ilAdi { get; set; }

        public byte plakaNo { get; set; }

        public int telefonKodu { get; set; }

        public int RowNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ilce> Ilce { get; set; }
    }
}
