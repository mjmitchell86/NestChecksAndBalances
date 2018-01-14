using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NestChecksAndBalances.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NestChecksAndBalances.Repositories
{
    public interface IUserRepository
    {
        void SaveUser(CabUser user);
        IEnumerable<CabUser> ListCabUsers();
        CabUser GetUser(string userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly DynamoDBOperationConfig _ddboc = new DynamoDBOperationConfig();

        public UserRepository(IAmazonDynamoDB dynamo)
        {
            _dbContext = new DynamoDBContext(dynamo);
        }

        public void SaveUser(CabUser user)
        {
            _dbContext.SaveAsync(user).Wait();
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