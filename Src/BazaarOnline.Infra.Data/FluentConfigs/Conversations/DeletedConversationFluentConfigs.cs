using BazaarOnline.Domain.Entities.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class DeletedConversationFluentConfigs : IEntityTypeConfiguration<DeletedConversation>
    {
        public void Configure(EntityTypeBuilder<DeletedConversation> builder)
        {
            builder.HasKey(m => m.Id);

            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<DeletedConversation> builder)
        {
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.DeleteDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureRelations(EntityTypeBuilder<DeletedConversation> builder)
        {
            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Conversation)
                .WithMany(c => c.DeletedConversations)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<DeletedConversation> builder)
        {
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<DeletedConversation> builder)
        {
        }
    }
}