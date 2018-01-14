using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Repositories;

namespace NestChecksAndBalances.Services
{
    public interface IUserService
    {

    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
    }
}
