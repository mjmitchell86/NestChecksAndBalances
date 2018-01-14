using NestChecksAndBalances.Utilities;
using System;

namespace NestChecksAndBalances.Models
{
    public class CabUser : BaseCabUser
    {
        public string UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public string CreatedDate { get; set; }
      

        public CabUser()
        {

        }

        public CabUser(string nestToken, string thermostatId, int targetTemperature, int ceilingTemp)
        {           
            UserId = Guid.NewGuid().ToString();
            NestToken = nestToken;
            ThermostatId = thermostatId;
            RoomTargetTemperature = targetTemperature;
            CeilingSetTemperature = ceilingTemp;
            CreatedDate = DateTime.Now.ToDynamoDbDateTime();
        }
    }

    public class BaseCabUser
    {
        public string NestToken { get; set; }
        public string ThermostatId { get; set; }
        public string UserName { get; set; }
        public int RoomTargetTemperature { get; set; }
        public int CeilingSetTemperature { get; set; }
    }
}