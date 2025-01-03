using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Persistence.EntityConfigurations;

public class CategoryAttributeConfiguration : IEntityTypeConfiguration<CategoryAttribute>
{
    public void Configure(EntityTypeBuilder<CategoryAttribute> builder)
    {
        builder.ToTable("AppCategoryAttributes");
        builder.HasKey(item => item.Id);
        
        builder.HasOne<Category>(item => item.Category)
            .WithMany(item => item.CategoryAttributes)
            .HasForeignKey(item => item.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Attribute>(item => item.Attribute)
            .WithMany(item => item.CategoryAttributes)
            .HasForeignKey(item => item.AttributeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}