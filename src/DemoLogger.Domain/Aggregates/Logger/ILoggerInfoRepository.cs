using Common.Infrastructure.Core;

namespace Logger.Domain.Aggregates.Logger;

public interface ILoggerInfoRepository : IRepository<LoggerInfo, long>
{
}