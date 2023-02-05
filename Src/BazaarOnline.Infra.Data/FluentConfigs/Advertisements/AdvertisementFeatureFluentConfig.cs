using BazaarOnline.Domain.Entities.Advertisements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Advertisements
{
    public class AdvertisementFeatureFluentConfig : IEntityTypeConfiguration<AdvertisementFeature>
    {
        public void Configure(EntityTypeBuilder<AdvertisementFeature> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<AdvertisementFeature> builder)
        {
            builder.Property(af => af.Value)
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<AdvertisementFeature> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<AdvertisementFeature> builder)
        {
            builder.HasOne(af => af.Advertisement)
                .WithMany(a => a.AdvertisementFeatures)
                .HasForeignKey(af => af.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(af => af.CategoryFeature)
                .WithMany(cf => cf.AdvertisementFeatures)
                .HasForeignKey(af => af.CategoryFeatureId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureIndexes(EntityTypeBuilder<AdvertisementFeature> builder)
        {
        }
    }
}