using Logger.Domain.Aggregates.Logger.Events;

namespace DemoLogger.Application.IntergrationEvents;

public interface ISubscriberService
{
    void CheckReceivedMessage(TestSendLoggerInfoEvent @event);
}