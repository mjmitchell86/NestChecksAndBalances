using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Logging;
using NestChecksAndBalances.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NestChecksAndBalances.Repositories
{
    public interface IUserRepository
    {
        void SaveUser(CabUser xuUser);
        IEnumerable<CabUser> ListCabUsers();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly IAmazonDynamoDB _dynamo;
        private readonly DynamoDBOperationConfig _ddboc = new DynamoDBOperationConfig();
        private readonly ILogger _logger;

        public UserRepository(ILogger<UserRepository> logger, IAmazonDynamoDB dynamo)
        {
            _dbContext = new DynamoDBContext(dynamo);
            _logger = logger;
        }

        public void SaveUser(CabUser xuUser)
        {
            _dbContext.SaveAsync(xuUser).Wait();
        }

        public CabUser GetUser(string userId)
        {
            return _dbContext.LoadAsync<CabUser>(userId).Result;
        }

        public IEnumerable<CabUser> ListCabUsers()
        {
            var userList = new List<CabUser>();
            try
            {
                _ddboc.IndexName = "IsActive_IDX";

                var results = _dbContext.QueryAsync<CabUser>(true, _ddboc);

                do
                {
                    var set = results.GetNextSetAsync().Result;

                    if (set != null || set.Any())
                    {
                        userList.AddRange(set);
                    }

                } while (!results.IsDone);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userList;
        }
    }
}