using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Filters;
using NestChecksAndBalances.Models;
using NestChecksAndBalances.Services;
using System;

namespace NestChecksAndBalances.Controllers
{
    [ServiceFilter(typeof(UserAuthFilter))]
    [Route("api/temperature")]
    public class TemperatureController : Controller
    {
        private readonly ILogger _logger;
        private readonly ITemperatureService _temperatureService;

        public TemperatureController(ILogger<TemperatureController> logger, ITemperatureService temperatureService)
        {
            _logger = logger;
            _temperatureService = temperatureService;
        }

        [HttpPost("{temp}")]
        public IActionResult Post([FromQuery]string userId, int temp)
        {           
            try
            {
                var user = RouteData.Values["user"] as CabUser;
                return new OkObjectResult(_temperatureService.RecordTemperature(user, temp));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Saving Temperature: " + ex);
                return BadRequest("Error Saving Temperature.  Please try again.");
            }
        }
    }
}