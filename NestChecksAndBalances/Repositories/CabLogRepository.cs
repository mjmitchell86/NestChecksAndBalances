using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NestChecksAndBalances.Models;

namespace NestChecksAndBalances.Repositories
{
    public interface ICabLogRepository
    {
        void SaveLog(CabLog xuLog);
    }

    public class CabLogRepository : ICabLogRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly IAmazonDynamoDB _dynamo;
        private readonly DynamoDBOperationConfig _ddboc = new DynamoDBOperationConfig();

        public CabLogRepository( IAmazonDynamoDB dynamo)
        {
            _dbContext = new DynamoDBContext(dynamo);
        }
        public void SaveLog(CabLog xuLog)
        {
            _dbContext.SaveAsync(xuLog).Wait();
        }
    }
}