using BazaarOnline.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BazaarOnline.Infra.Data.FluentConfigs.Categories
{
    public class CategoryFluentConfig
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(u => u.Id);


            ConfigureProperties(builder);
            ConfigureRelations(builder);
            ConfigureIndexes(builder);
            ConfigureQueryFilters(builder);
        }


        private void ConfigureProperties(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Title)
                .HasMaxLength(32)
                .IsRequired();
        }

        private void ConfigureQueryFilters(EntityTypeBuilder<Category> builder)
        {
        }

        private void ConfigureRelations(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.ChildCategories)
                .WithOne(c2 => c2.ParentCategory)
                .HasForeignKey(c2 => c2.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.ParentCategory)
                .WithMany(c2 => c2.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Advertisements)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Category> builder)
        {
        }
    }
}