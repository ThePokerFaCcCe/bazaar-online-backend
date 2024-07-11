using BazaarOnline.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class UserAdvertisementHistoryFluentConfigs : IEntityTypeConfiguration<UserAdvertisementHistory>
    {
        public void Configure(EntityTypeBuilder<UserAdvertisementHistory> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<UserAdvertisementHistory> builder)
        {
            builder.Property(u => u.CreateDate)
                .IsRequired()
                ;

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue<bool>(false);
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<UserAdvertisementHistory> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<UserAdvertisementHistory> builder)
        {
            builder.HasOne(u => u.Advertisement)
                .WithMany(a => a.WatchedUsers)
                .HasForeignKey(u => u.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.WatchedAdvertisements)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<UserAdvertisementHistory> builder)
        {
        }
    }
}