using Common.Infrastructure.Core.Behavior.Exceptions;
using Common.Infrastructure.Core.Behavior.Vaildation;
using Dapper;
using DemoLogger.Application.Commands;
using DemoLogger.Application.IntergrationEvents;
using DemoLogger.Infrastructure.Databases;
using DemoLogger.Infrastructure.Databases.Behaviors;
using Logger.Domain.Aggregates.Logger;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoLogger.API.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Mediator 注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerContextTransactionBehavior<,>));
        services.AddMediatR(typeof(LoggerInfo).Assembly, typeof(Program).Assembly, typeof(AddLoggInfoCommand).Assembly);
        return services;
    }

    /// <summary>
    /// 接入事件总线
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISubscriberService,SubscriberService>();
        services.AddCap(options =>
        {
            //options.UseEntityFramework<LoggerContext>();
            options.UseMySql(configuration.GetConnectionString("MySql"));
            options.UseRabbitMQ(rabbitMqOptions => { configuration.GetSection("RabbitMQ").Bind(rabbitMqOptions); });
            // options.UseDashboard();
        });

        return services;
    }

}