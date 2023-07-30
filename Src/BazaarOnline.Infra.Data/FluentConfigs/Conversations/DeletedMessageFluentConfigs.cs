using BazaarOnline.Domain.Entities.Conversations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs;

public class DeletedMessageFluentConfigs : IEntityTypeConfiguration<DeletedMessage>
{
    public void Configure(EntityTypeBuilder<DeletedMessage> builder)
    {
        builder.HasKey(m => m.Id);

        ConfigureProperties(builder);
        ConfigureRelations(builder);
        ConfigureIndexes(builder);
        ConfigureQueryFilters(builder);
    }


    private void ConfigureProperties(EntityTypeBuilder<DeletedMessage> builder)
    {
        builder.Property(c => c.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");
        
        builder.Property(c => c.DeleteDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");
    }

    private void ConfigureRelations(EntityTypeBuilder<DeletedMessage> builder)
    {
        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Conversation)
            .WithMany()
            .HasForeignKey(c => c.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Message)
            .WithMany()
            .HasForeignKey(c => c.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureIndexes(EntityTypeBuilder<DeletedMessage> builder)
    {
    }

    private void ConfigureQueryFilters(EntityTypeBuilder<DeletedMessage> builder)
    {
    }
}