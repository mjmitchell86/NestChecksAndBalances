using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Services;

namespace NestChecksAndBalances.Filters
{
    public class UserAuthFilter : ActionFilterAttribute
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserAuthFilter(IUserService userService, ILogger<UserAuthFilter> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
          if (context.ActionArguments.ContainsKey("userId"))
            {
                var userId = string.Empty;

                try
                {
                    userId = context.ActionArguments["userId"] as string;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        var user = _userService.GetUser(userId);
                        if (user == null)
                        {
                            context.Result = new NotFoundObjectResult("UserId : " + userId + " - Not Found.");
                        }
                        context.RouteData.Values.Add("user", user);
                        return;
                    }
                }
                catch
                {
                    _logger.LogInformation("No UserId provided in the query");
                }
            }

            context.Result = new BadRequestObjectResult("Denied.  userId must be provided in query string.");
            return;
        }
    }
}