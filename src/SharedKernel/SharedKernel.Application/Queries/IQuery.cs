namespace SharedKernel.Application.Queries;

public interface IQuery<out TResponse> where TResponse : notnull;