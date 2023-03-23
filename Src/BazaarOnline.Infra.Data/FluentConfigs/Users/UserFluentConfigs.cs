using BazaarOnline.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class UserFluentConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id)
                .IsRequired()
                .HasMaxLength(36)
                .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(11);

            builder.Property(u => u.DisplayName)
                .IsRequired()
                .HasMaxLength(60)
                .HasDefaultValue("˜ÇÑÈÑ ÈÇÒÇÑ");

            builder.Property(u => u.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue<bool>(false);

            builder.Property(u => u.IsPhoneNumberActive)
                .IsRequired()
                .HasDefaultValue<bool>(false);

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue<bool>(false);
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<User> builder)
        {
            builder.HasQueryFilter(u => u.IsDeleted == false);
        }

        private void ConfigureRelations(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.ValidationCodes)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);


            builder.HasMany(u => u.Advertisements)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.WatchedAdvertisements)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.AdvertisementNotes)
                .WithOne(un => un.User)
                .HasForeignKey(un => un.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.AdvertisementBookmarks)
                .WithOne(ub => ub.User)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}