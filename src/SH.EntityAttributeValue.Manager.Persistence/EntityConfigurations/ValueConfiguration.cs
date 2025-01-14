using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SH.EntityAttributeValue.Manager.Domain.Entities;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Persistence.EntityConfigurations;

public class ValueConfiguration : IEntityTypeConfiguration<Value>
{
    public void Configure(EntityTypeBuilder<Value> builder)
    {
        builder.ToTable("AppValues");
        builder.HasKey(item => item.Id);
        
        builder.Property(item => item.Content).IsRequired();
        builder.Property(item => item.AsString).IsRequired(false);
        builder.Property(item => item.AsBoolean).IsRequired(false);
        builder.Property(item => item.AsDate).IsRequired(false);
        builder.Property(item => item.AsInteger).IsRequired(false);
        builder.Property(item => item.AsDecimal).IsRequired(false);
        
        builder.HasOne<Product>(item => item.Product)
            .WithMany(item => item.Values)
            .HasForeignKey(item => item.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Attribute>(item => item.Attribute)
            .WithMany()
            .HasForeignKey(item => item.AttributeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}