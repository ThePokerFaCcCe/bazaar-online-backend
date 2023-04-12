using BazaarOnline.Domain.Entities.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class ConversationFluentConfigs : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(m => m.Id);

            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<Conversation> builder)
        {
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureRelations(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasOne(c => c.Advertisement)
                .WithMany()
                .HasForeignKey(c => c.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Conversation> builder)
        {
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<Conversation> builder)
        {
        }
    }
}