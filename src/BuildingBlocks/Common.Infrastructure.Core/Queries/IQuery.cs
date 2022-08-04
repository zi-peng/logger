using MediatR;

namespace Common.Infrastructure.Core.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}