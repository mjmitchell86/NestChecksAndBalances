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

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Retrieving CABUser List: " + ex);
                return BadRequest("Error retrieving user list.  Please try again.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if(!string.IsNullOrEmpty(id))
            try
            {
                var data = _userService.GetUser(id);

                if (data == null)
                {
                    return NotFound("User Not found.");
                }

                return new OkObjectResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Retrieving CABUser ID: " + id + " -- Ex: " + ex);
                return BadRequest("Error retrieving user.  Please try again.");
            }

            _logger.LogInformation("ID not passed in to User Get");
            return BadRequest("Error: Please pass in an ID");
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
                _logger.LogError("Error Saving CABUser: " + ex);
                return BadRequest("Error Saving new user.  Please try again.");
            }
        }       
    }
}