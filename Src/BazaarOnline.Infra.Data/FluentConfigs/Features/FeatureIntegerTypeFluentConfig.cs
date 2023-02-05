using BazaarOnline.Domain.Entities.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Features;

public class FeatureStringTypeFluentConfig
{
    public void Configure(EntityTypeBuilder<FeatureStringType> builder)
    {
        builder.HasKey(u => u.Id);


        ConfigureProperties(builder);
        ConfigureRelations(builder);
        ConfigureIndexes(builder);
        ConfigureQueryFilters(builder);
    }


    private void ConfigureProperties(EntityTypeBuilder<FeatureStringType> builder)
    {
        builder.Property(fs => fs.Placeholder)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(fs => fs.MaxLength)
            .HasDefaultValue(int.MaxValue)
            .IsRequired();

        builder.Property(fs => fs.MinLength)
            .HasDefaultValue(0)
            .IsRequired();
    }

    private void ConfigureQueryFilters(EntityTypeBuilder<FeatureStringType> builder)
    {
    }

    private void ConfigureRelations(EntityTypeBuilder<FeatureStringType> builder)
    {
        builder.HasMany(fs => fs.Features)
            .WithOne(f => f.StringType)
            .HasForeignKey(f => f.StringTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureIndexes(EntityTypeBuilder<FeatureStringType> builder)
    {
    }
}