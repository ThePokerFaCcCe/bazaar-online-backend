using BazaarOnline.Domain.Entities.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class BlocklistFluentConfigs : IEntityTypeConfiguration<Blocklist>
    {
        public void Configure(EntityTypeBuilder<Blocklist> builder)
        {
            builder.HasKey(m => m.Id);

            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<Blocklist> builder)
        {
            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired()
                ;
        }

        private void ConfigureRelations(EntityTypeBuilder<Blocklist> builder)
        {
            builder.HasOne(c => c.BlockedUser)
                .WithMany()
                .HasForeignKey(c => c.BlockedUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Blocker)
                .WithMany()
                .HasForeignKey(c => c.BlockerId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Blocklist> builder)
        {
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<Blocklist> builder)
        {
        }
    }
}