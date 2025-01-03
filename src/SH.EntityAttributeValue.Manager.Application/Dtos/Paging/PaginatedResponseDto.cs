namespace SH.EntityAttributeValue.Manager.Application.Dtos.Paging;

public class PaginatedResponseDto<T> : BasePageableModel
{
    private IList<T> _items;

    public IList<T> Items { get => _items ??= new List<T>(); set => _items = value; }
}

public class PaginatedRequestDto
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 25;

    public PaginatedRequestDto()
    {
        
    }

    public PaginatedRequestDto(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}