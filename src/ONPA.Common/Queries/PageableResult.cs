namespace ONPA.Common.Queries;

public class PageableResult<T> where T : class
{
    public IEnumerable<T> Result { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public PageableResult(IEnumerable<T> result, int total, int page,
        int pageSize)
    {
        this.Result = result;
        this.Total = total;
        this.Page = page;
        this.PageSize = pageSize;
    }
}