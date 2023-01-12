using BazaarOnline.Domain.Entities.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Maps
{
    public class CityFluentConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<City> builder)
        {
            builder.Property(c => c.AmarCode)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<City> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<City> builder)
        {
            builder.HasOne(c => c.Province)
                .WithMany(p => p.Cities)
                .HasForeignKey(c => c.ProvinceId);
        }

        private void ConfigureIndexes(EntityTypeBuilder<City> builder)
        {
            builder.HasIndex(c => c.AmarCode)
                .IsUnique();
        }
    }
}