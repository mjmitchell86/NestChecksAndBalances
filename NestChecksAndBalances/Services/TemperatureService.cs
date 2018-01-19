using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Integrations;
using NestChecksAndBalances.Models;
using NestChecksAndBalances.Models.Enums;
using NestChecksAndBalances.Repositories;
using System;

namespace NestChecksAndBalances.Services
{
    public interface ITemperatureService
    {
        CabLog RecordTemperature(CabUser user, int temp);
    }

    public class TemperatureService : ITemperatureService
    {
        private readonly ICabLogRepository _cabLogRepository;
        private readonly INestAPI _nestAPI;
        private readonly ILogger _logger;

        public TemperatureService(ILogger<TemperatureService> logger,ICabLogRepository cabLogRepository, INestAPI nestAPI)
        {
            _cabLogRepository = cabLogRepository;
            _nestAPI = nestAPI;
            _logger = logger;
        }

        public CabLog RecordTemperature(CabUser user, int temp)
        {
            var tempLog = new CabLog(user.UserId, LogAction.IncomingTemperature, temp);
            _cabLogRepository.SaveLog(tempLog);

            //Find temperature information of house
            var houseConditions = new NestObject();
            try
            {
                houseConditions = _nestAPI.GetCurrentHouseConditions(user.NestToken, user.ThermostatId);
                _logger.LogInformation("Current Nest House Ambient Temperature is: " + houseConditions.ambient_temperature_f);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Calling Nest API to get current house conditions: " + ex.Message, ex);
            }

            //If Nest is Away, return
            if(houseConditions.hvac_mode == "eco")
            {
                return tempLog;
            }

            //Check if there has been a NestSet created with this UserID in the past 20 minutes OR NestFan Log created with this UserID in the past 60 minutes.  If yes, just return tempLog

            //If Nest temperature is greater than the incoming temp given && the Nest Temperature is less than the set CeilingTemperature  -----> raise the Nest Temp by 1 degree.

            //If Nest Temperature is greater than the incoming temp given && the Nest Temperature is Equeal to or Greater than the ceilingTemperature && Nest Fan is not active --> Turn on Nest Fan for 1 Hour.

            return tempLog;
        }
    }
}