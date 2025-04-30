namespace Domain.Filters;

public class CustomerFilter
{
    public string? FullName { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
