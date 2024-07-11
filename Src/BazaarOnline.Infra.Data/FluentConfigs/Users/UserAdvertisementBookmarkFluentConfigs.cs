using BazaarOnline.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class UserAdvertisementBookmarkFluentConfigs : IEntityTypeConfiguration<UserAdvertisementBookmark>
    {
        public void Configure(EntityTypeBuilder<UserAdvertisementBookmark> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<UserAdvertisementBookmark> builder)
        {
            builder.Property(u => u.CreateDate)
                .IsRequired()
                ;
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<UserAdvertisementBookmark> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<UserAdvertisementBookmark> builder)
        {
            builder.HasOne(u => u.Advertisement)
                .WithMany(a => a.UserBookmarks)
                .HasForeignKey(u => u.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.AdvertisementBookmarks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<UserAdvertisementBookmark> builder)
        {
        }
    }
}