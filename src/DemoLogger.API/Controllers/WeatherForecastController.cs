using DemoLogger.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoLogger.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator _mediator;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var loggInfoCommand = new AddLoggInfoCommand
        {
            Title = "标题",
            Content = "内容"
        };
        var command = await _mediator.Send(loggInfoCommand);

        using (_logger.BeginScope("Adding item {ItemId} for user {UserName}", "1", "2"))
        {
            _logger.LogInformation("Summaries的值是：{Summaries}", Summaries);
            //throw new ApplicationException("出错了！");
            var list = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
            _logger.LogInformation("WeatherForecast的值是：{@list}", new { list });

            return list;
        }
    }
}