using Common.Domain.Abstractions;

namespace Logger.Domain.Aggregates.Logger.Events;

public class TestSendLoggerInfoEvent : IDomainEvent
{
    public LoggerInfo LoggerInfo { get; }

    public TestSendLoggerInfoEvent(LoggerInfo loggerInfo)
    {
        LoggerInfo = loggerInfo;
    }
}