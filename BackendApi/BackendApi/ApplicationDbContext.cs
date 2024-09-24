using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { }
        
        public DbSet<Settings> Settings { get; set; }
    }
}
