using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SH.EntityAttributeValue.Manager.Domain.Entities;

namespace SH.EntityAttributeValue.Manager.Persistence.EntityConfigurations;

public class AttributeOptionConfiguration : IEntityTypeConfiguration<AttributeOption>
{
    public void Configure(EntityTypeBuilder<AttributeOption> builder)
    {
        builder.ToTable("AppAttributeOptions");
        builder.HasKey(item => item.Id);
        
        builder.Property(item => item.Value).HasMaxLength(256).IsRequired();
        
        builder.HasOne(item => item.Attribute)
            .WithMany(item => item.AttributeOptions)
            .HasForeignKey(item => item.AttributeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}