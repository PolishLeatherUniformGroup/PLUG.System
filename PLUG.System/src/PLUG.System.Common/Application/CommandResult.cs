namespace PLUG.System.Common.Application;

public class CommandResult
{
    public Guid AggreagteId { get; private set; }
    public bool IsFailure { get; }
    public bool IsSuccess => !IsFailure;
    private readonly List<string> _errors = new List<string>();
    public IEnumerable<string> Errors => this._errors;
    
    private bool _isValid;
    public bool IsValid => this._isValid;
   
    public static CommandResult InValid(params string[] errors)
    {
        var result = new CommandResult(errors)
        {
            _isValid = false
        };
        return result;

    }

    protected CommandResult(string[] errors)
    {
        this.AggreagteId = Guid.Empty;
        this._errors.AddRange(errors);
        this.IsFailure = true;
    }

    private CommandResult(Guid id)
    {
        this.AggreagteId = id;
        this.IsFailure = false;
        this._isValid = true;
    }

    public static CommandResult Success(Guid id)
    {
        return new CommandResult(id);
    }

    public static CommandResult Fail(params string[] errors)
    {
        return new CommandResult(errors)
        {
            _isValid = true
        };
    }
    

    public static implicit operator CommandResult(Exception exception)
    {
        return Fail(exception.Message);
    }
    
    

    public static implicit operator CommandResult(Guid id)
    {
        return Success(id);
    }

}