namespace KuaforRandevuSistemi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YetkiSayfa")]
    public partial class YetkiSayfa
    {
        public int yetkiSayfaID { get; set; }

        [StringLength(100)]
        public string controllerName { get; set; }

        [StringLength(100)]
        public string actionName { get; set; }

        public bool? yonetici { get; set; }

        public bool? personel { get; set; }

        public bool? musteri { get; set; }
    }
}
