using MediatR;

namespace Common.Domain.Abstractions;

/// <summary>
/// 领域事件的抽象
/// </summary>
/// <typeparam name="TDomainEvent"></typeparam>
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : INotification
{
     
}