using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ApiCenter.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Generate random weather forecast
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Generate random weather forecast , can set rangeEnd
        /// </summary>
        /// <param name="rangeEnd">Set end range of array</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /WeatherForecast
        ///     {
        ///        "rangeEnd": 1
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public IEnumerable<WeatherForecast> Post(int rangeEnd = 3)
        {
            var rng = new Random();
            return Enumerable.Range(1, rangeEnd).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
                .ToArray();
        }

        /// <summary>
        /// Get current weather information
        /// </summary>
        /// <param name="wf">weather parameter</param>
        /// <returns></returns>
        /// <response code="200">Return a weather forecast</response>
        /// <response code="400">If the value of WeatherForecast is null</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string GetWeatherInfo(WeatherForecast wf)
        {
            var weatherForecast = new WeatherForecast()
            {
                Date = DateTime.Now,
                TemperatureC = wf.TemperatureC,
                Summary = wf.Summary
            };
            return JsonSerializer.Serialize(weatherForecast);
        }
    }
}