namespace SharedKernel.Application.Commands;

// Void / No response
public struct Unit
{
    public static readonly Unit Value = new();
}

public interface ICommand<out TResult>;

public interface ICommand : ICommand<Unit>;