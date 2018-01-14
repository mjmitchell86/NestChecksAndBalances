using Amazon.Util;
using System;

namespace NestChecksAndBalances.Utilities
{
    public static class DateTimeUtil
    {
        public static string ToDynamoDbDateTime(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString(AWSSDKUtils.ISO8601BasicDateTimeFormat);
        }
    }
}