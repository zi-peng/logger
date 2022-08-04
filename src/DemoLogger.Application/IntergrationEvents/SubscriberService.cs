using DotNetCore.CAP;
using Logger.Domain.Aggregates.Logger.Events;
using Microsoft.Extensions.Logging;

namespace DemoLogger.Application.IntergrationEvents;

public class SubscriberService : ISubscriberService, ICapSubscribe
{
    private readonly ILogger<SubscriberService> _logger;

    public SubscriberService(ILogger<SubscriberService> logger)
    {
        _logger = logger;
    }

    [CapSubscribe("test.sendLogger")]
    public void CheckReceivedMessage(TestSendLoggerInfoEvent @event)
    {
        _logger.LogInformation("标题是{@LoggerInfo}",@event.LoggerInfo);
    }
}