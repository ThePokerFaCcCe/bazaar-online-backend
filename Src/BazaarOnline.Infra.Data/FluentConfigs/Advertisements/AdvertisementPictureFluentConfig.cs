using BazaarOnline.Domain.Entities.Advertisements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Advertisements
{
    public class AdvertisementPicturePictureFluentConfig : IEntityTypeConfiguration<AdvertisementPicture>
    {
        public void Configure(EntityTypeBuilder<AdvertisementPicture> builder)
        {
            builder.HasKey(ap => ap.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<AdvertisementPicture> builder)
        {
            builder.Property(ap => ap.PictureName)
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<AdvertisementPicture> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<AdvertisementPicture> builder)
        {
            builder.HasOne(ap => ap.Advertisement)
                .WithMany(ap => ap.Pictures)
                .HasForeignKey(ap => ap.AdvertisementId);
        }

        private void ConfigureIndexes(EntityTypeBuilder<AdvertisementPicture> builder)
        {
        }
    }
}