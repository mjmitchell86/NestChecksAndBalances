using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Models;
using NestChecksAndBalances.Services;

namespace NestChecksAndBalances.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(new CabUser("testToken", "thermId", 3, 5));
        }
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]BaseCabUser cabUser)
        {
            return new OkObjectResult(new CabUser(cabUser.NestToken, cabUser.ThermostatId,cabUser.RoomTargetTemperature, cabUser.CeilingSetTemperature));
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
