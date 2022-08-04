using DemoLogger.API.Extensions;
using DemoLogger.AspNetExtensions.Logging;
using DemoLogger.Infrastructure.Databases;
using Serilog;

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Host.UseSerilogConfiguration();
   
  
    builder.Services.AddMysqlDatabase(builder.Configuration.GetConnectionString("MySql")).AddRepositories();
  
    builder.Services.AddMediatorServices();


    builder.Services.AddEventBus(builder.Configuration);
    var app = builder.Build();
 
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}