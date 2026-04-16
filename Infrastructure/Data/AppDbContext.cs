using ApiDotNet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
