using System;
using System.Collections.Generic;
using TAM.Service.Data.Entities;

namespace TAM.Service.Data.Repository
{
    public interface IDataStore
    {
        List<User> GetUsers();
        User GetUser(Guid id);
        bool TryRegisteringUser(User user, LoginDetails userCredential);
        Tuple<bool,Guid> IsUserValid(LoginDetails loginDetails);
        List<Asset> GetUserAssets(Guid userId);

        bool TryRegisteringAsset(Asset asset);
        Tuple<bool,string> TryAllocatingAssetToUser(string assetSerialNumber, Guid userId);
        bool TryUpdatingAssetDetails(Asset asset);
        List<Asset> GetAllAssets();
        Asset GetAsset(string serialNumber);

        bool TryAddingTeam(Team team);
        List<Team> GetAllTeams();
        Team GetTeam(string teamName);
        List<Asset> GetTeamAssets(string teamName);
        List<User> GetTeamMembers(string teamName);

        bool SaveChanges();
    }
}