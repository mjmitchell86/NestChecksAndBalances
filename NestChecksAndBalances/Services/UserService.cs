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

        public IEnumerable<CabUser> ListUsers()
        {
            return _userRepository.ListCabUsers();
        }

        public CabUser SaveUser(BaseCabUser baseUser)
        {
            var xuUser = new CabUser(baseUser.NestToken, baseUser.ThermostatId, baseUser.RoomTargetTemperature, baseUser.CeilingSetTemperature, baseUser.UserName, baseUser.PhoneNumber);
            _userRepository.SaveUser(xuUser);
            _cabLogRepository.SaveLog(new CabLog(xuUser.UserId, LogAction.UserCreated));
            return xuUser;
        }
    }
}
