using Microsoft.EntityFrameworkCore;
using Proyecto_Grupal.Models;

namespace Proyecto_Grupal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<DeptEmp> DeptEmps { get; set; }
        public DbSet<DeptManager> DeptManagers { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<LogAuditoriaSalario> LogAuditoriaSalarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeptEmp>()
                .HasKey(d => new { d.EmpNo, d.DeptNo, d.FromDate });

            modelBuilder.Entity<DeptManager>()
                .HasKey(d => new { d.EmpNo, d.DeptNo, d.FromDate });

            modelBuilder.Entity<Title>()
                .HasKey(t => new { t.EmpNo, t.TitleName, t.FromDate });

            modelBuilder.Entity<Salary>()
                .HasKey(s => new { s.EmpNo, s.FromDate });
        }
    }
}