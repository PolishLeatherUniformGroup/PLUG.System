namespace PLUG.System.Common.Application;

public class CollectionResult<T> where T : class
{
    private List<T> _value;
    public List<T> Value => this._value;

    private bool _hasError;
    public bool HasError => this._hasError && this._errors.Any();

    public bool IsValid => !this._hasError && !this._errors.Any();
    
    private readonly List<string> _errors = new();
    public IEnumerable<string> Errors => this._errors;
    
    private CollectionResult(List<T> value)
    {
        this._value = value;
        this._hasError = false;
    }

    private CollectionResult()
    {
        this._value = null;
        this._hasError = false;
    }

    private CollectionResult(params string[] errors)
    {
        this._errors.AddRange(errors);
    }

    public static CollectionResult<T> FromValue(IEnumerable<T> value)
    {
        return new CollectionResult<T>(value.ToList());
    }
    
    public static CollectionResult<T> FromException(Exception exception)
    {
        return new CollectionResult<T>(exception.Message);
    }

    public CollectionResult<T> WithErrors(params string[] errors)
    {
        this._errors.AddRange(errors);
        return this;
    }
}