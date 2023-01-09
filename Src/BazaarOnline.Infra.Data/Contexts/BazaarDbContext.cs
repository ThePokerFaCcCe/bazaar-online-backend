using BazaarOnline.Domain.Entities.Categories;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Infra.Data.FluentConfigs;
using Microsoft.EntityFrameworkCore;

namespace BazaarOnline.Infra.Data.Contexts
{
    public class BazaarDbContext : DbContext
    {
        public BazaarDbContext(DbContextOptions<BazaarDbContext> options) : base(options) { }

        #region DB Sets

        #region Users

        public DbSet<User> Users { get; set; }
        public DbSet<ValidationCode> ActiveCodes { get; set; }

        #endregion

        #region Categories

        public DbSet<Category> Categories { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region FluentConfigs

            builder.ApplyConfigurationsFromAssembly(typeof(UserFluentConfigs).Assembly);

            #endregion
        }
    }
}
