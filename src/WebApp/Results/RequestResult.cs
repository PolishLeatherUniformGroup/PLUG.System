namespace PLUG.WebApp.Results;

public record RequestResult<T>
{
    public Result Outcome { get; set; }
    public string? Message { get; set; }
    public T? Value { get; set; }

    public RequestResult(T value)
    {
        Outcome = Result.Success;
    }
};