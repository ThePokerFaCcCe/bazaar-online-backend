using BazaarOnline.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class ActiveCodeFluentConfigs : IEntityTypeConfiguration<ValidationCode>
    {
        public void Configure(EntityTypeBuilder<ValidationCode> builder)
        {
            builder.HasKey(m => m.Id);

            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<ValidationCode> builder)
        {
            builder.Property(m => m.Code)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(m => m.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(m => m.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.ExpireDate)
                .IsRequired();
        }

        private void ConfigureRelations(EntityTypeBuilder<ValidationCode> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany(u => u.ValidationCodes)
                .HasForeignKey(u => u.UserId);
        }

        private void ConfigureIndexes(EntityTypeBuilder<ValidationCode> builder)
        {
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<ValidationCode> builder)
        {
            builder.HasQueryFilter(m => m.ExpireDate > DateTime.Now);
        }
    }
}