using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NestChecksAndBalances.Models;

namespace NestChecksAndBalances.Repositories
{
    public interface ICabLogRepository
    {
        void SaveLog(CabLog log);
    }

    public class CabLogRepository : ICabLogRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly DynamoDBOperationConfig _ddboc = new DynamoDBOperationConfig();

        public CabLogRepository(IAmazonDynamoDB dynamo)
        {
            _dbContext = new DynamoDBContext(dynamo);
        }
        public void SaveLog(CabLog log)
        {
            _dbContext.SaveAsync(log).Wait();
        }
    }
}