using BazaarOnline.Domain.Entities.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Features;

public class FeatureIntegerTypeFluentConfig
{
    public void Configure(EntityTypeBuilder<FeatureIntegerType> builder)
    {
        builder.HasKey(u => u.Id);


        ConfigureProperties(builder);
        ConfigureRelations(builder);
        ConfigureIndexes(builder);
        ConfigureQueryFilters(builder);
    }


    private void ConfigureProperties(EntityTypeBuilder<FeatureIntegerType> builder)
    {
        builder.Property(fi => fi.Placeholder)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(fi => fi.Maximum)
            .HasDefaultValue(long.MaxValue)
            .IsRequired();

        builder.Property(fi => fi.Minimum)
            .HasDefaultValue(0)
            .IsRequired();
    }

    private void ConfigureQueryFilters(EntityTypeBuilder<FeatureIntegerType> builder)
    {
    }

    private void ConfigureRelations(EntityTypeBuilder<FeatureIntegerType> builder)
    {
        builder.HasMany(fi => fi.Features)
            .WithOne(f => f.IntegerType)
            .HasForeignKey(f => f.IntegerTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureIndexes(EntityTypeBuilder<FeatureIntegerType> builder)
    {
    }
}