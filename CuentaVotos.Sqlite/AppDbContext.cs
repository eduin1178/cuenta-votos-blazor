using CuentaVotos.Entiies.Account;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Repository;
using CuentaVotos.Sqlite.Configuration;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace CuentaVotos.Sqlite
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }

        public async Task<ResultBase> SaveAsync()
        {
            try
            {
                var count = await base.SaveChangesAsync();
                return new ResultBase
                {
                    IsSuccess = true,
                    Count = count,
                    Code = 1,
                    Message = "OK"
                };

            }
            catch (Exception ex)
            {
                return new ResultBase
                {
                    IsSuccess = false,
                    Count = 0,
                    Code = 0,
                    Message = "Error",
                    Exception = ex
                };
            }
        }

    }
}
