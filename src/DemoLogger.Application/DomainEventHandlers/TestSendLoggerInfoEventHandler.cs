using Common.Domain.Abstractions;
using DotNetCore.CAP;
using Logger.Domain.Aggregates.Logger.Events;

namespace DemoLogger.Application.DomainEventHandlers;

public class TestSendLoggerInfoEventHandler : IDomainEventHandler<TestSendLoggerInfoEvent>
{
    private readonly ICapPublisher _capPublisher;

    public TestSendLoggerInfoEventHandler(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public async Task Handle(TestSendLoggerInfoEvent notification, CancellationToken cancellationToken)
    {
        await _capPublisher.PublishAsync("test.sendLogger", notification);
    }
}