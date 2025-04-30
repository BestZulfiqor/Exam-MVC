namespace Domain.Filters;

public class OrderFilter
{
    public DateTime? FromOrderDate { get; set; }
    public DateTime? ToOrderDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
