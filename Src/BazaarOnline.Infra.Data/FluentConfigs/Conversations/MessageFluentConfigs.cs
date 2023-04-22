using BazaarOnline.Domain.Entities.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class MessageFluentConfigs : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<Message> builder)
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

            builder.Property(c => c.UpdateDate)
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.Text)
                .HasMaxLength(512)
                .IsRequired()
                .HasDefaultValue(string.Empty);

            builder.Property(c => c.AttachmentJson)
                .IsRequired(false);

            builder.Property(c => c.IsSeen)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.IsEdited)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.AttachmentType)
                .HasConversion<string>()
                .IsRequired()
                .HasDefaultValue(MessageAttachmentTypeEnum.NoAttachment);
        }

        private void ConfigureRelations(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(c => c.Sender)
                .WithMany()
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(c => c.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.ReplyTo)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ReplyToId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Message> builder)
        {
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<Message> builder)
        {
        }
    }
}