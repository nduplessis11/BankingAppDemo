namespace SharedKernel.Application.Utilities
{
    public sealed record Option<T>
    {
        private readonly T? _value;

        public bool IsSome { get; }
        public bool IsNone => !IsSome;

        private Option(T? value, bool isSome)
        {
            _value = value;
            IsSome = isSome;
        }

        public static Option<T> Some(T value)
        {
            return new Option<T>(value, true);
        }

        public static Option<T> None() => new(default, false);

        public TUnwrap Match<TUnwrap>(Func<T, TUnwrap> onSome, Func<TUnwrap> onNone)
        {
            if (IsSome)
            {
                return onSome(_value!);
            }
            return onNone();
        }

        public Option<TOut> Map<TOut>(Func<T, TOut> mappingFunction)
        {
            if (IsSome)
            {
                return Option<TOut>.Some(mappingFunction(_value!));
            }
            return Option<TOut>.None();
        }

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> bindingFunction)
        {
            if (IsSome)
            {
                return bindingFunction(_value!);
            }
            return Option<TOut>.None();
        }

        public T GetValueOrDefault(T defaultValue = default!)
        {
            return IsSome ? _value! : defaultValue;
        }

        public Option<T> OrElse(T fallbackValue)
        {
            return IsSome ? this : Option<T>.Some(fallbackValue);
        }

        public Option<T> OrElse(Func<Option<T>> fallbackOption)
        {
            return IsSome ? this : fallbackOption();
        }
    }
}
