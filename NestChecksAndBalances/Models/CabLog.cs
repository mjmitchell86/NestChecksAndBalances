using Amazon.DynamoDBv2.DataModel;
using NestChecksAndBalances.Models.Enums;
using NestChecksAndBalances.Utilities;
using System;

namespace NestChecksAndBalances.Models
{
    [DynamoDBTable("CABLog")]
    public class CabLog
    {
        public string UserId { get; set; }
        public string LogType { get; set; }
        public string CreatedDate { get; set; }
        public int? Value { get; set; }

        public CabLog()
        {

        }

        public CabLog(string userId, LogAction logType)
        {
            UserId = userId;
            LogType = logType.ToString();
            CreatedDate = DateTime.Now.ToDynamoDbDateTime();
        }

        public CabLog(string userId, LogAction logType, int value)
        {
            UserId = userId;
            LogType = logType.ToString();
            CreatedDate = DateTime.Now.ToDynamoDbDateTime();
            Value = value;
        }
    }
}