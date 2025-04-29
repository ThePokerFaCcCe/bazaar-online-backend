using BazaarOnline.Domain.Entities.Advertisements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Advertisements
{
    public class AdvertisementFluentConfig : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(a => a.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(a => a.Address)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Longitude)
                .HasPrecision(3, 7)
                .IsRequired(false);

            builder.Property(a => a.Latitude)
                .HasPrecision(3, 7)
                .IsRequired(false);

            builder.Property(a => a.ShowExactCoordinates)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(a => a.StatusType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.StatusReason)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.ContactType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(a => a.PriceValue)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(a => a.PriceType)
                .HasConversion<short>()
                .HasDefaultValue(AdvertisementPriceTypeEnum.Agreement)
                .IsRequired();

            builder.Property(a => a.CreateDate)

                .IsRequired();

            builder.Property(a => a.UpdateDate)

                .IsRequired();

            builder.Ignore(a => a.IsDeleted);
            builder.Ignore(a => a.HasCoordinates);
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasQueryFilter(a =>
                a.StatusType != AdvertisementStatusTypeEnum.DeletedByAdmin ||
                a.StatusType != AdvertisementStatusTypeEnum.DeletedByUser);
        }

        private void ConfigureRelations(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasOne(a => a.Category)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Province)
                .WithMany(p => p.Advertisements)
                .HasForeignKey(a => a.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.City)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithMany(u => u.Advertisements)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Pictures)
                .WithOne(ap => ap.Advertisement)
                .HasForeignKey(ap => ap.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.AdvertisementFeatures)
                .WithOne(af => af.Advertisement)
                .HasForeignKey(ap => ap.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.WatchedUsers)
                .WithOne(u => u.Advertisement)
                .HasForeignKey(u => u.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.UserNotes)
                .WithOne(u => u.Advertisement)
                .HasForeignKey(u => u.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.UserBookmarks)
                .WithOne(u => u.Advertisement)
                .HasForeignKey(u => u.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Advertisement> builder)
        {
        }
    }
}