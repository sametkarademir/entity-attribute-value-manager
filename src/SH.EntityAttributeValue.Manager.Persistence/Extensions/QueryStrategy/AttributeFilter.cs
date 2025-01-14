using SH.EntityAttributeValue.Manager.Application.Enums;
using SH.EntityAttributeValue.Manager.Domain.Enums;

namespace SH.EntityAttributeValue.Manager.Persistence.Extensions.QueryStrategy;

public class AttributeFilter
{
    public Guid AttributeId { get; set; }
    public string Content { get; set; } = null!;
    public DataTypes DataType { get; set; }
    public OperatorTypes OperatorType { get; set; }
    public LogicTypes LogicType { get; set; }
}