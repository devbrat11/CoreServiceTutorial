using System;
using System.Collections.Generic;
using System.Linq;
using CoreService.Data.Entities;

namespace CoreService.Data
{
    public class InMemorySqlDataStore:IDataStore
    {
        private readonly CoreServiceContext _coreServiceContext;

        public InMemorySqlDataStore(CoreServiceContext coreServiceContext)
        {
            _coreServiceContext = coreServiceContext;
        }

        public List<User> GetUsers()
        {
            return _coreServiceContext.Users.ToList();
        }

        public User GetUser(Guid id)
        {
            var users = _coreServiceContext.Users;
            return users.First(x => x.Id.Equals(id));
        }

        public void AddUser(User user)
        {
            _coreServiceContext.Users.Add(user);
        }

        public void AddAsset(Guid userId, Asset asset)
        {
            var assets = _coreServiceContext.Assets.ToList();
            if (assets.Any(x => x.SerialNumber == asset.SerialNumber))
            {
                return;
            }

            _coreServiceContext.Assets.Add(asset);
        }

        public bool SaveChanges()
        {
            _coreServiceContext.SaveChanges();
            return true;
        }
        
    }
}
