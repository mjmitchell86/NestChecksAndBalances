using NestChecksAndBalances.Models;
using NestChecksAndBalances.Models.Enums;
using NestChecksAndBalances.Repositories;
using System.Collections.Generic;

namespace NestChecksAndBalances.Services
{
    public interface IUserService
    {
        CabUser SaveUser(BaseCabUser baseUser);
        IEnumerable<CabUser> ListUsers();
        CabUser GetUser(string userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICabLogRepository _cabLogRepository;

        public UserService(ICabLogRepository cabLogRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _cabLogRepository = cabLogRepository;
        }

        public CabUser GetUser(string userId)
        {
            return _userRepository.GetUser(userId);
        }

        public IEnumerable<CabUser> ListUsers()
        {
            return _userRepository.ListCabUsers();
        }

        public CabUser SaveUser(BaseCabUser baseUser)
        {
            var user = new CabUser(baseUser.NestToken, baseUser.ThermostatId, baseUser.RoomTargetTemperature, baseUser.CeilingSetTemperature, baseUser.UserName, baseUser.PhoneNumber);
            _userRepository.SaveUser(user);
            _cabLogRepository.SaveLog(new CabLog(user.UserId, LogAction.UserCreated));
            return user;
        }
    }
}
