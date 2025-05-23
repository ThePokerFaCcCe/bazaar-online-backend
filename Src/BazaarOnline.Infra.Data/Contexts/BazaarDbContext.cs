using BazaarOnline.Domain.Entities.Advertisements;
using BazaarOnline.Domain.Entities.Categories;
using BazaarOnline.Domain.Entities.Conversations;
using BazaarOnline.Domain.Entities.Features;
using BazaarOnline.Domain.Entities.Maps;
using BazaarOnline.Domain.Entities.Users;
using BazaarOnline.Infra.Data.FluentConfigs;
using Microsoft.EntityFrameworkCore;

namespace BazaarOnline.Infra.Data.Contexts
{
    public class BazaarDbContext : DbContext
    {
        public BazaarDbContext(DbContextOptions<BazaarDbContext> options) : base(options)
        {
        }

        #region DB Sets

        #region Users

        public DbSet<User> Users { get; set; }
        public DbSet<ValidationCode> ActiveCodes { get; set; }
        public DbSet<UserAdvertisementNote> UserAdvertisementNotes { get; set; }
        public DbSet<UserAdvertisementHistory> UserAdvertisementHistories { get; set; }
        public DbSet<UserAdvertisementBookmark> UserAdvertisementBookmark { get; set; }

        #endregion

        #region Categories

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFeature> CategoryFeatures { get; set; }

        #endregion

        #region Maps

        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }

        #endregion

        #region Advertisements

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementPicture> AdvertisementPictures { get; set; }
        public DbSet<AdvertisementFeature> AdvertisementFeatures { get; set; }

        #endregion

        #region Features

        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureStringType> FeatureStringTypes { get; set; }
        public DbSet<FeatureIntegerType> FeatureIntegerTypes { get; set; }
        public DbSet<FeatureSelectType> FeatureSelectTypes { get; set; }

        #endregion

        #region Conversations

        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<DeletedConversation> DeletedConversations { get; set; }
        public DbSet<Blocklist> Blocklist { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var customSchema = System.Environment.GetEnvironmentVariable("DATABASE_SCHEMA_NAME");
            if (!string.IsNullOrWhiteSpace(customSchema))
            {
                builder.HasDefaultSchema(customSchema);
            }
            #region FluentConfigs

            builder.ApplyConfigurationsFromAssembly(typeof(UserFluentConfigs).Assembly);

            #endregion
        }
    }
}