namespace Fluent.Result;

public enum ErrorType
{
    Failure = 0,
    Validation = 1
}

public record Error(ErrorType Type, string Description);
