using BazaarOnline.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs
{
    public class UserAdvertisementNoteFluentConfigs : IEntityTypeConfiguration<UserAdvertisementNote>
    {
        public void Configure(EntityTypeBuilder<UserAdvertisementNote> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<UserAdvertisementNote> builder)
        {
            builder.Property(u => u.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("getdate()");

            builder.Property(u => u.Note)
                .HasMaxLength(255)
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<UserAdvertisementNote> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<UserAdvertisementNote> builder)
        {
            builder.HasOne(u => u.Advertisement)
                .WithMany(a => a.UserNotes)
                .HasForeignKey(u => u.AdvertisementId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.AdvertisementNotes)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureIndexes(EntityTypeBuilder<UserAdvertisementNote> builder)
        {
        }
    }
}