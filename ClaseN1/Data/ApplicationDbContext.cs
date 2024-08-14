using ClaseN1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClaseN1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <Empleados> Empleados{ get; set; }
        public DbSet <TipoEmpleado> TipoEmpleados{ get; set; }

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Empleados>( e=>e.ToTable("Empleado").HasKey(p=>p.Id));
            modelBuilder.Entity<TipoEmpleado>(entity => entity.ToTable("TipoEmpleado").HasKey(p => p.Id));
        }
    }
}
