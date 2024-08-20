using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Application.Commands;

public class CommandDispatcher(IServiceProvider serviceProvider)
{
    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var handler = GetHandler<ICommandHandler<TCommand>>(typeof(TCommand).Name);
        await handler.HandleAsync(command, cancellationToken);
    }

    public async Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResponse>
    {
        var handler = GetHandler<ICommandHandler<TCommand, TResponse>>(typeof(TCommand).Name);
        return await handler.HandleAsync(command, cancellationToken);
    }

    private THandler GetHandler<THandler>(string commandName)
    {
        var handler = serviceProvider.GetService<THandler>();
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for type {commandName}");
        }
        return handler;
    }
}