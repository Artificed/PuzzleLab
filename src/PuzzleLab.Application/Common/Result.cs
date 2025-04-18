namespace PuzzleLab.Application.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected internal Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error ?? Error.None;
    }

    public static Result Success() => new Result(true, Error.None);
    public static Result Failure(Error error) => new Result(false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value for failed result.");

    protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, Error.None);
    public new static Result<T> Failure(Error error) => new Result<T>(default, false, error);
}