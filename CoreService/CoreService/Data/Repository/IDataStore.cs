using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CoreService.Data.Entities;
using CoreService.Models;

namespace CoreService.Data.Repository
{
    public interface IDataStore
    {
        List<UserOutputDto> GetUsers();
        UserOutputDto GetUser(Guid id);
        bool TryRegisteringUser(UserEntities userEntity);
        Tuple<bool,Guid> IsUserValid(string emailId, string password);
        List<Asset> GetUserAssets(Guid userId);
        bool TryRegisteringAsset(Asset asset);
        Tuple<bool,string> TryAllocatingAssetToUser(Guid userId,Asset asset);
        bool TryUpdatingAssetDetails(Asset asset);
        List<Asset> GetAllAssets();
        Asset GetAsset(string serialNumber);
        bool SaveChanges();
    }
}