namespace SharedKernel.Application.Utilities;

    public record Result<TSuccess, TFailure>
        where TSuccess : notnull
        where TFailure : notnull
    {
        public TSuccess? Value { get; }
        public TFailure? Error { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        private Result(TSuccess? value, TFailure? error, bool isSuccess)
        {
            Value = value;
            Error = error;
            IsSuccess = isSuccess;
        }

        public static Result<TSuccess, TFailure> Success(TSuccess value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            return new Result<TSuccess, TFailure>(value, default, true);
        }

        public static Result<TSuccess, TFailure> Failure(TFailure error)
        {
            if (error is null) throw new ArgumentNullException(nameof(error));
            return new Result<TSuccess, TFailure>(default, error, false);
        }

        public Result<TNewSuccess, TFailure> Map<TNewSuccess>(Func<TSuccess, TNewSuccess> func)
            where TNewSuccess : notnull
        {
            if (IsSuccess && Value is not null) return Result<TNewSuccess, TFailure>.Success(func(Value));
            return Result<TNewSuccess, TFailure>.Failure(Error!);
        }

        public Result<TSuccess, TNewFailure> MapError<TNewFailure>(Func<TFailure, TNewFailure> func)
            where TNewFailure : notnull
        {
            if (IsFailure && Error is not null) return Result<TSuccess, TNewFailure>.Failure(func(Error));
            return Result<TSuccess, TNewFailure>.Success(Value!);
        }

        public Result<TNewSuccess, TFailure> Bind<TNewSuccess>(Func<TSuccess, Result<TNewSuccess, TFailure>> func)
            where TNewSuccess : notnull
        {
            if (IsSuccess && Value is not null) return func(Value);
            return Result<TNewSuccess, TFailure>.Failure(Error!);
        }

        public Result<TSuccess, TFailure> OnSuccess(Action<TSuccess> action)
        {
            if (IsSuccess && Value is not null) action(Value);
            return this;
        }

        public Result<TSuccess, TFailure> OnFailure(Action<TFailure> action)
        {
            if (IsFailure && Error is not null) action(Error);
            return this;
        }

        public T Match<T>(Func<TSuccess, T> onSuccess, Func<TFailure, T> onFailure)
        {
            return IsSuccess && Value is not null ? onSuccess(Value) : onFailure(Error!);
        }
    }