using SH.EntityAttributeValue.Manager.Application.Enums;

namespace SH.EntityAttributeValue.Manager.Application.Dtos.Dynamic;

public class Sort(string field, DirectionTypes dir)
{
    public Sort() : this(string.Empty, DirectionTypes.Asc)
    {
    }

    public string Field { get; set; } = field;
    public DirectionTypes Dir { get; set; } = dir;
}