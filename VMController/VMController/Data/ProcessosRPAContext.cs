using Microsoft.EntityFrameworkCore;
using VMController.Models;

namespace VMController.Data
{
    public class ProcessosRPAContext : DbContext
    {
        public ProcessosRPAContext(DbContextOptions<ProcessosRPAContext> options)
            : base(options)
        {
        }

        public DbSet<ProcessosRPA> ProcessosRPA { get; set; }
    }
}