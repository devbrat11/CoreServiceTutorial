using System;
using System.Collections.Generic;
using CoreService.Data.Entities;

namespace CoreService.Data
{
    public interface IDataStore
    {
        List<User> GetUsers();
        User GetUser(Guid id);
        void AddUser(User user);
        void AddAsset(Guid userId, Asset asset);
        bool SaveChanges();
    }
}