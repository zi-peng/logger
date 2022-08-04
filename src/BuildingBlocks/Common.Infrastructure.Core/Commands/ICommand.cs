using MediatR;

namespace Common.Infrastructure.Core.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}