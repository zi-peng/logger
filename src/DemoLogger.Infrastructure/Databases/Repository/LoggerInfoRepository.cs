using Common.Infrastructure.Core;
using Logger.Domain.Aggregates.Logger;

namespace DemoLogger.Infrastructure.Databases.Repository;

public class LoggerInfoRepository : Repository<LoggerInfo, long, LoggerContext>, ILoggerInfoRepository
{
    public LoggerInfoRepository(LoggerContext context) : base(context)
    {
    }
}