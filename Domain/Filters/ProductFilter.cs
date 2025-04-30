namespace Domain.Filters;

public class ProductFilter
{
    public string? Name { get; set; }
    public decimal? FromPrice { get; set; }
    public decimal? ToPrice { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
