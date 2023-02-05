using BazaarOnline.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Categories;

public class CategoryFeatureFluentConfig
{
    public void Configure(EntityTypeBuilder<CategoryFeature> builder)
    {
        builder.HasKey(u => u.Id);


        ConfigureProperties(builder);
        ConfigureRelations(builder);
        ConfigureIndexes(builder);
        ConfigureQueryFilters(builder);
    }


    private void ConfigureProperties(EntityTypeBuilder<CategoryFeature> builder)
    {
        builder.Property(cf => cf.IsRequired)
            .IsRequired();

        builder.Property(cf => cf.IsFilterable)
            .IsRequired();

        builder.Property(cf => cf.IsShownInList)
            .IsRequired();

        builder.Property(cf => cf.SortNumber)
            .IsRequired();
    }

    private void ConfigureQueryFilters(EntityTypeBuilder<CategoryFeature> builder)
    {
    }

    private void ConfigureRelations(EntityTypeBuilder<CategoryFeature> builder)
    {
        builder.HasOne(cf => cf.Category)
            .WithMany(c => c.CategoryFeatures)
            .HasForeignKey(cf => cf.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cf => cf.Feature)
            .WithMany(f => f.CategoryFeatures)
            .HasForeignKey(cf => cf.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(cf => cf.AdvertisementFeatures)
            .WithOne(af => af.CategoryFeature)
            .HasForeignKey(af => af.CategoryFeatureId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureIndexes(EntityTypeBuilder<CategoryFeature> builder)
    {
    }
}