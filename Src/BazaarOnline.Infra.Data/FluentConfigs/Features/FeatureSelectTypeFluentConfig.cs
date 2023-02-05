using BazaarOnline.Domain.Entities.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Features;

public class FeatureSelectTypeFluentConfig
{
    public void Configure(EntityTypeBuilder<FeatureSelectType> builder)
    {
        builder.HasKey(u => u.Id);


        ConfigureProperties(builder);
        ConfigureRelations(builder);
        ConfigureIndexes(builder);
        ConfigureQueryFilters(builder);
    }


    private void ConfigureProperties(EntityTypeBuilder<FeatureSelectType> builder)
    {
        builder.Property(fs => fs.Options)
            .IsRequired();

        builder.Ignore(fs => fs.OptionsList);
    }

    private void ConfigureQueryFilters(EntityTypeBuilder<FeatureSelectType> builder)
    {
    }

    private void ConfigureRelations(EntityTypeBuilder<FeatureSelectType> builder)
    {
        builder.HasMany(fs => fs.Features)
            .WithOne(f => f.SelectType)
            .HasForeignKey(f => f.SelectTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureIndexes(EntityTypeBuilder<FeatureSelectType> builder)
    {
    }
}