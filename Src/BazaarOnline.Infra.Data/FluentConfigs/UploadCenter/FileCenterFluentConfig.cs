using BazaarOnline.Domain.Entities.UploadCenter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.UploadCenter
{
    public class FileCenterFluentConfig : IEntityTypeConfiguration<FileCenter>
    {
        public void Configure(EntityTypeBuilder<FileCenter> builder)
        {
            builder.HasKey(fc => fc.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<FileCenter> builder)
        {
            builder.Property(fc => fc.FileName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(fc => fc.ExtraProperties)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(fc => fc.FileType)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(fc => fc.SizeKB)
                .IsRequired();

            builder.Property(fc => fc.UsageType)
                .HasConversion<string>()
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<FileCenter> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<FileCenter> builder)
        {
        }

        private void ConfigureIndexes(EntityTypeBuilder<FileCenter> builder)
        {
        }
    }
}