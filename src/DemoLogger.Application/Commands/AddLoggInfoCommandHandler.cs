using Common.Infrastructure.Core.Commands;
using Logger.Domain.Aggregates.Logger;
using Logger.Domain.Aggregates.Logger.Events;
using MediatR;

namespace DemoLogger.Application.Commands;

public class AddLoggInfoCommandHandler : ICommandHandler<AddLoggInfoCommand, bool>
{
    private readonly ILoggerInfoRepository _loggerInfoRepository;

    public AddLoggInfoCommandHandler(ILoggerInfoRepository loggerInfoRepository)
    {
        _loggerInfoRepository = loggerInfoRepository;
    }

    public async Task<bool> Handle(AddLoggInfoCommand request, CancellationToken cancellationToken)
    {
        var loggerInfo = new LoggerInfo("内容", "标题");

        loggerInfo.SendAddTestEvent();
        
        var flag = await _loggerInfoRepository.AddAsync(loggerInfo, cancellationToken);
        return true;
    }
}