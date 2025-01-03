using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = SH.EntityAttributeValue.Manager.Domain.Entities.Attribute;

namespace SH.EntityAttributeValue.Manager.Persistence.EntityConfigurations;

public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
{
    public void Configure(EntityTypeBuilder<Attribute> builder)
    {
        builder.ToTable("AppAttributes");
        builder.HasKey(item => item.Id);
        
        builder.Property(item => item.Name).HasMaxLength(256).IsRequired();
        builder.Property(item => item.DataType).IsRequired();
    }
}