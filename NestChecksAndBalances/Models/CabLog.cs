using NestChecksAndBalances.Utilities;
using System;

namespace NestChecksAndBalances.Models
{
    public class CabLog
    {
        public string UserId { get; set; }
        public string LogType { get; set; }
        public string CreatedDate { get; set; }

        public CabLog()
        {

        }

        public CabLog(string userId, string LogType)
        {
            UserId = UserId;
            LogType = LogType;
            CreatedDate = DateTime.Now.ToDynamoDbDateTime();
        }
    }
}