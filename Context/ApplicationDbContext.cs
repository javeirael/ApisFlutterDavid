using ApisFlutter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApisFlutter.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor para pasar la configuración al contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TipoProducto> tipoProducto { get; set; }
        public DbSet<Proveedor> proveedor { get; set; }
        public DbSet<Producto> producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
