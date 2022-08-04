using Common.Domain.Abstractions;
using Logger.Domain.Aggregates.Logger.Events;

namespace Logger.Domain.Aggregates.Logger;

public class LoggerInfo : Entity<long>, IAggregateRoot
{
    public string Content { get; private set; }
    public string Title { get; private set; }

    public LoggerInfo(string content, string title)
    {
        Content = content;
        Title = title;
    }

    /// <summary>
    /// 发送领域事件
    /// </summary>
    public void SendAddTestEvent()
    {
        AddDomainEvent(new TestSendLoggerInfoEvent(this));
    }
}