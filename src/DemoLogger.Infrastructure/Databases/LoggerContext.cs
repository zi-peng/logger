using Common.Infrastructure.Core;
using DemoLogger.Infrastructure.Databases.EntityConfigurations;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoLogger.Infrastructure.Databases;

/// <summary>
/// migrations : dotnet ef migrations add init -o Databases/Migrations -c LoggerContext
/// script : dotnet ef migrations script
/// remove : dotnet ef migrations remove
/// update : dotnet ef database update
/// </summary>
public class LoggerContext : EFContext
{
    public LoggerContext(DbContextOptions<LoggerContext> options) : base(options,null,null)
    {
        
    }
    public LoggerContext(DbContextOptions<LoggerContext> options, IMediator mediator, ICapPublisher capPublisher) : base(options, mediator, capPublisher)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region 注册领域模型跟数据库的映射关系

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LoggerEntityTypeConfigurations).Assembly);

        #endregion

        base.OnModelCreating(modelBuilder);
    }


   
}