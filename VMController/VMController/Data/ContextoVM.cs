using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VMController.Models;

namespace VMController.Data
{
    public class ContextoVM : DbContext
    {
        public ContextoVM(DbContextOptions<ContextoVM> options) : base(options)
        {
            
        }

        public DbSet<VirtualMachine> VirtualMachines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
	        {
	            modelBuilder.Entity<VirtualMachine>(entity =>
	            {
	                entity.HasKey(e => e.idVM);
	            });
	
	            base.OnModelCreating(modelBuilder);
	        }

    }
}
