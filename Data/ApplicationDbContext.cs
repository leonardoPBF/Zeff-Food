using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zeff_Food.Models.Entitys;

namespace Zeff_Food.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
              
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<ItemFactura> ItemsFactura { get; set; }
        public DbSet<Producto> Productos { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)        
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Factura>()
                .HasMany(f => f.Items)
                .WithOne(i => i.Factura)
                .HasForeignKey(i => i.FacturaId);

            modelBuilder.Entity<ItemFactura>()
                .HasOne(i => i.Producto)
                .WithMany()
                .HasForeignKey(i => i.ProductoId);

            // Configuraciones adicionales según sea necesario
        }
    }
}