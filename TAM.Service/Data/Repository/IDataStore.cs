using System;
using System.Collections.Generic;
using TAMService.Data.Entities;

namespace TAMService.Data.Repository
{
    public interface IDataStore
    {
        List<User> GetUsers();
        User GetUser(Guid id);
        bool TryRegisteringUser(User user);
        Tuple<bool,Guid> IsUserValid(string emailId, string password);
        List<Asset> GetUserAssets(Guid userId);

        bool TryRegisteringAsset(Asset asset);
        Tuple<bool,string> TryAllocatingAssetToUser(string assetSerialNumber, Guid userId);
        bool TryUpdatingAssetDetails(Asset asset);
        List<Asset> GetAllAssets();
        Asset GetAsset(string serialNumber);

        bool TryAddingTeam(Team team);
        List<Team> GetAllTeamsInformation();
        Team GetTeamInformation(string teamName);
        List<Asset> GetTeamAssets(string teamName);
        List<User> GetTeamMembers(string teamName);


        bool SaveChanges();
    }
}