namespace Domain.Filters;

public class OrderItemFilter
{
    public int? FromQuantity { get; set; }
    public int? ToQuantity { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
