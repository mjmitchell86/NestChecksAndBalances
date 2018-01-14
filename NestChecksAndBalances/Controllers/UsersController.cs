using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Models;
using NestChecksAndBalances.Services;
using System;
using System.Linq;

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
            try
            {
                var data = _userService.ListUsers();

                if (!data.Any())
                {
                    return NotFound("No users found.");
                }

                return new OkObjectResult(_userService.ListUsers());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Retrieving CABUser List: " + ex.Message);
                return BadRequest("Error retrieving user list.  Please try again.");
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post([FromBody]BaseCabUser cabUser)
        {
            try
            {
                return new OkObjectResult(_userService.SaveUser(cabUser));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Saving CABUser: " + ex.Message);
                return BadRequest("Error Saving new user.  Please try again.");
            }
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