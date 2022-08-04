using Common.Infrastructure.Core;
using Dapper;
using DemoLogger.Infrastructure.Databases.Behaviors;
using DemoLogger.Infrastructure.Databases.Repository;
using Logger.Domain.Aggregates.Logger;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DemoLogger.Infrastructure.Databases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMysqlDatabase(this IServiceCollection serviceCollection,
        string connectionString)
    {
        //匹配下划线字段 user_id
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        //注入数据库
        serviceCollection.AddDbContext<LoggerContext>(
            options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    o => { o.EnableRetryOnFailure(); });
            });

        
        return serviceCollection;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = typeof(LoggerInfoRepository).Assembly;
        //FromAssemblyOf获取类的程序集
        services.Scan(x => x.FromAssemblies(assembly)
            //1.模糊匹配类名
            //.AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
            //2.按照命名空间进行注入
            .AddClasses(c => c.InExactNamespaceOf<LoggerInfoRepository>())
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsMatchingInterface()
            .WithScopedLifetime());
        return services;
    }
}