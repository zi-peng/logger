using Common.Infrastructure.Core.Commands;

namespace DemoLogger.Application.Commands;

public class AddLoggInfoCommand : ICommand<bool>
{
    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string? Content { get; set; }
}