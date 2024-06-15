/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace Fluent.Result;

public sealed class Result<T>
{
    public Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    public Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public T Value { get; }
    public Error Error { get; }
    public bool IsSuccess { get; private set;}

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
}
