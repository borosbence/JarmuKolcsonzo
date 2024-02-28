using JarmuKolcsonzo.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace JarmuKolcsonzo.Model
{
    public partial class JKContext : DbContext
    {
        public JKContext()
        {
        }

        public JKContext(DbContextOptions<JKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<JarmuTipus> jarmu_tipusok { get; set; } = null!;
        public virtual DbSet<Jarmu> jarmuvek { get; set; } = null!;
        public virtual DbSet<Rendeles> rendelesek { get; set; } = null!;
        public virtual DbSet<Ugyfel> ugyfelek { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;database=jarmukolcsonzo", ServerVersion.Parse("10.4.24-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Jarmu>(entity =>
            {
                entity.HasOne(d => d.tipus)
                    .WithMany(p => p.jarmuvek)
                    .HasForeignKey(d => d.tipus_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("jarmuvek_ibfk_1");
                // Kiegészítés
                entity.HasIndex(x => x.rendszam)
                    .IsUnique();
            });

            modelBuilder.Entity<Rendeles>(entity =>
            {
                entity.HasOne(d => d.jarmu)
                    .WithMany(p => p.rendelesek)
                    .HasForeignKey(d => d.jarmu_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rendelesek_ibfk_2");

                entity.HasOne(d => d.ugyfel)
                    .WithMany(p => p.rendelesek)
                    .HasForeignKey(d => d.ugyfel_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rendelesek_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
