using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimulazioneMVC.Model
{
    public partial class AutoriContext : DbContext
    {
        public AutoriContext()
        {
        }

        public AutoriContext(DbContextOptions<AutoriContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autori> Autori { get; set; }
        public virtual DbSet<Presentazioni> Presentazioni { get; set; }
        public virtual DbSet<Registrazioni> Registrazioni { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimulazioneMVC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registrazioni>(entity =>
            {
                entity.HasKey(e => new { e.IdPresentazione, e.IdAutore });

                entity.HasOne(d => d.IdAutoreNavigation)
                    .WithMany(p => p.Registrazioni)
                    .HasForeignKey(d => d.IdAutore)
                    .HasConstraintName("FK_Registrazioni_Autori");

                entity.HasOne(d => d.IdPresentazioneNavigation)
                    .WithMany(p => p.Registrazioni)
                    .HasForeignKey(d => d.IdPresentazione)
                    .HasConstraintName("FK_Registrazioni_Presentazioni");
            });
        }
    }
}
