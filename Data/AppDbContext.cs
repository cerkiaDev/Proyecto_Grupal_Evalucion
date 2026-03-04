using Microsoft.EntityFrameworkCore;
using Proyecto_Grupal.Models;

namespace Proyecto_Grupal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EmpleadoDepartamento> EmpleadoDepartamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmpleadoDepartamento>(b =>
            {
                b.HasIndex(e => new { e.EmpleadoId, e.FromDate, e.ToDate });
                b.HasCheckConstraint("CK_EmpleadoDepartamento_ToDate_After_FromDate", "[ToDate] IS NULL OR [ToDate] >= [FromDate]");

                b.HasOne(ed => ed.Empleado)
                    .WithMany(e => e.EmpleadoDepartamentos)
                    .HasForeignKey(ed => ed.EmpleadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(ed => ed.Departamento)
                    .WithMany(d => d.EmpleadoDepartamentos)
                    .HasForeignKey(ed => ed.DepartamentoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
