using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CoreService.Data.Entities;
using CoreService.Models;
using CoreService.Models.BaseDto;
using CoreService.Models.ResultDto;

namespace CoreService.Data.Repository
{
    public interface IDataStore
    {
        List<UserResultDto> GetUsers();
        UserResultDto GetUser(Guid id);
        bool TryRegisteringUser(UserEntities userEntity);
        Tuple<bool,Guid> IsUserValid(string emailId, string password);
        List<AssetDto> GetUserAssets(Guid userId);

        bool TryRegisteringAsset(Asset asset);
        Tuple<bool,string> TryAllocatingAssetToUser(string assetSerialNumber, Guid userId);
        bool TryUpdatingAssetDetails(Asset asset);
        List<AssetOutputDto> GetAllAssets();
        AssetOutputDto GetAsset(string serialNumber);

        bool TryAddingTeam(Team team);
        List<Team> GetAllTeamsInformation();
        Team GetTeamInformation(string teamName);
        List<AssetOutputDto> GetTeamAssets(string teamName);
        List<UserResultDto> GetTeamMembers(string teamName);

        bool SaveChanges();
    }
}