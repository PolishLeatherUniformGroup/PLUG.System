namespace PLUG.System.Common.Application;

public class SingleResult<T> where T : class
{
    private T? _value;
    public T? Value => this._value;

    private bool _hasError;
    public bool HasError => this._hasError && this._errors.Any();

    public bool IsValid => !this._hasError && !this._errors.Any();
    
    private readonly List<string> _errors = new();
    public IEnumerable<string> Errors => this._errors;
    
    private SingleResult(T value)
    {
        this._value = value;
        this._hasError = false;
    }

    private SingleResult()
    {
        this._value = null;
        this._hasError = false;
    }

    private SingleResult(params string[] errors)
    {
        this._errors.AddRange(errors);
    }

    public static SingleResult<T> NotFound()
    {
        return new SingleResult<T>();
    }

    public static SingleResult<T> FromValue(T value)
    {
        return new SingleResult<T>(value);
    }
    
    public static SingleResult<T> FromException(Exception exception)
    {
        return new SingleResult<T>(exception.Message);
    }

    public SingleResult<T> WithErrors(params string[] errors)
    {
        this._errors.AddRange(errors);
        return this;
    }
}