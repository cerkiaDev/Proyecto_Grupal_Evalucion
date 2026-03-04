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
        public DbSet<DeptManager> DeptManagers { get; set; }

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

            modelBuilder.Entity<DeptManager>(b =>
            {
                b.HasIndex(d => new { d.DepartamentoId, d.FromDate, d.ToDate });
                b.HasCheckConstraint("CK_DeptManager_ToDate_After_FromDate", "[ToDate] IS NULL OR [ToDate] >= [FromDate]");

                b.HasOne(dm => dm.Empleado)
                    .WithMany(e => e.DeptManagers)
                    .HasForeignKey(dm => dm.EmpleadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(dm => dm.Departamento)
                    .WithMany(d => d.DeptManagers)
                    .HasForeignKey(dm => dm.DepartamentoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
