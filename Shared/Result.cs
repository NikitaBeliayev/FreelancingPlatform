namespace Shared
{
    public class Result<TValue>: Result
    {
        public TValue? Value { get; }

        protected Result(TValue? value, bool isSuccess, Error error): base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<TValue> Success(TValue? value) => new(value, true, Error.None);
        public static Result<TValue> Failure(TValue? value, Error error) => new(default, false, error);
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
    }
}
