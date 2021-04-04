using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KuaforRandevuSistemi.Models
{
    public partial class KuaforContext : DbContext
    {
        public KuaforContext()
            : base("name=KuaforContext")
        {
        }

        public virtual DbSet<Firma> Firma { get; set; }
        public virtual DbSet<Gider> Gider { get; set; }
        public virtual DbSet<GiderTur> GiderTur { get; set; }
        public virtual DbSet<Hizmet> Hizmet { get; set; }
        public virtual DbSet<Il> Il { get; set; }
        public virtual DbSet<Ilce> Ilce { get; set; }
        public virtual DbSet<Izin> Izin { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<Randevu> Randevu { get; set; }
        public virtual DbSet<RandevuHizmet> RandevuHizmet { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Yetki> Yetki { get; set; }
        public virtual DbSet<YetkiSayfa> YetkiSayfa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firma>()
                .Property(e => e.telefon)
                .IsFixedLength();

            modelBuilder.Entity<Gider>()
                .Property(e => e.tutar)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Hizmet>()
                .Property(e => e.ad)
                .IsFixedLength();

            modelBuilder.Entity<Hizmet>()
                .Property(e => e.ucret)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Hizmet>()
                .HasMany(e => e.RandevuHizmet)
                .WithRequired(e => e.Hizmet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Il>()
                .HasMany(e => e.Ilce)
                .WithRequired(e => e.Il)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.telefonNo)
                .IsFixedLength();

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.maas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.telefonNo)
                .IsFixedLength();

            modelBuilder.Entity<Randevu>()
                .HasMany(e => e.RandevuHizmet)
                .WithRequired(e => e.Randevu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RandevuHizmet>()
                .Property(e => e.ucret)
                .HasPrecision(19, 4);
        }
    }
}
