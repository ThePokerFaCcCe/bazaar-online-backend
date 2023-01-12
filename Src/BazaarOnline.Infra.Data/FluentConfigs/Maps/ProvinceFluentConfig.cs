using BazaarOnline.Domain.Entities.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Maps
{
    public class ProvinceFluentConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<Province> builder)
        {
            builder.Property(p => p.AmarCode)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<Province> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<Province> builder)
        {
            builder.HasMany(p => p.Cities)
                .WithOne(c => c.Province)
                .HasForeignKey(c => c.ProvinceId);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Province> builder)
        {
            builder.HasIndex(p => p.AmarCode)
                .IsUnique();
        }
    }
}