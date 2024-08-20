namespace SharedKernel.Application.Commands;

// Void / No response
public struct Unit
{
    public static readonly Unit Value = new Unit();
}

public interface ICommand<out TResponse>;

public interface ICommand : ICommand<Unit>;