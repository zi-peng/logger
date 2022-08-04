using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DemoLogger.Infrastructure.Databases.Design;

public class DesignDbContextFactory : IDesignTimeDbContextFactory<LoggerContext>
{
    private const string connectionString = "server=localhost;port=3306;user id=root;password=123456;database=logger;charset=utf8mb4;ConnectionReset=false;";

    public LoggerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LoggerContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new LoggerContext(optionsBuilder.Options);
    }
}