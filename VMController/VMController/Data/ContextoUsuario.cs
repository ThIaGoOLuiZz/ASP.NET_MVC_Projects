using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VMController.Models;

namespace VMController.Data
{
    public class ContextoUsuario : DbContext
    {
        public ContextoUsuario(DbContextOptions<ContextoUsuario> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
