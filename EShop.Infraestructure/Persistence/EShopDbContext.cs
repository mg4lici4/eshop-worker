using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infraestructure.Persistence
{
    public class EShopDbContext(DbContextOptions<EShopDbContext> options) : DbContext(options)
    {
        public DbSet<SesionEntity> Sesiones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SesionEntity>(entity =>
            {
                entity.ToTable("SESION");
                entity.HasKey(e => e.IdSesion);

                entity.Property(e => e.IdSesion)
                    .HasColumnName("ID_SESION")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO")
                    .IsRequired();

                entity.Property(e => e.Jti)
                    .HasColumnName("JTI")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.FechaExpiracion)
                    .HasColumnName("FECHA_EXPIRACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnName("FECHA_ACTUALIZACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v!, DateTimeKind.Utc));

                entity.HasIndex(e => e.Jti)
                    .IsUnique();
            });
        }
    }
}
