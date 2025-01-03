using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SH.EntityAttributeValue.Manager.Domain.Entities;


namespace SH.EntityAttributeValue.Manager.Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("AppProducts");
        builder.HasKey(item => item.Id);
        
        builder.Property(item => item.Name).HasMaxLength(256).IsRequired();
        
        builder.HasOne<Category>(item => item.Category)
            .WithMany()
            .HasForeignKey(item => item.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}