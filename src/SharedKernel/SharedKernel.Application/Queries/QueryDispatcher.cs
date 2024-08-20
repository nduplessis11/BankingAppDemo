using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Application.Queries;

public class QueryDispatcher(IServiceProvider serviceProvider)
{
    public async Task<TResponse> DispatchAsync<TQuery, TResponse>(TQuery query,
        CancellationToken cancellationToken = default) 
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
        var handler = serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>();
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler registered for {typeof(TQuery).Name}");
        }
        
        return await handler.HandleAsync(query, cancellationToken);
    }
}