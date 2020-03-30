using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace CoreService.Data
{
    public interface IDataStore
    {
        void AddUser();
    }

    public class NoSqlDataStore:IDataStore
    {
        private Container _container;

        public NoSqlDataStore(CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public void AddUser()
        {
            throw new NotImplementedException();
        }
    }
}
