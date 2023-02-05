using BazaarOnline.Domain.Entities.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Features;

public class FeatureFluentConfig
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.HasKey(u => u.Id);


        ConfigureProperties(builder);
        ConfigureRelations(builder);
        ConfigureIndexes(builder);
        ConfigureQueryFilters(builder);
    }


    private void ConfigureProperties(EntityTypeBuilder<Feature> builder)
    {
        builder.Property(f => f.Name)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(f => f.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(f => f.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(f => f.Position)
            .HasConversion<string>()
            .IsRequired();


        builder.Ignore(f => f.TypeObject);
    }

    private void ConfigureQueryFilters(EntityTypeBuilder<Feature> builder)
    {
    }

    private void ConfigureRelations(EntityTypeBuilder<Feature> builder)
    {
        builder.HasOne(f => f.IntegerType)
            .WithMany(fi => fi.Features)
            .HasForeignKey(f => f.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.StringType)
            .WithMany(fs => fs.Features)
            .HasForeignKey(f => f.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.SelectType)
            .WithMany(fs => fs.Features)
            .HasForeignKey(f => f.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.CategoryFeatures)
            .WithOne(cf => cf.Feature)
            .HasForeignKey(cf => cf.FeatureId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureIndexes(EntityTypeBuilder<Feature> builder)
    {
    }
}