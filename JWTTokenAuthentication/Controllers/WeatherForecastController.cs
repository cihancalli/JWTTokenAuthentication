using JWTTokenAuthentication.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTTokenAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private IJWTAuthenticationManager _manager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJWTAuthenticationManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredential userCredential)
        {
            var token = _manager.Authenticate(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }

    public class UserCredential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
