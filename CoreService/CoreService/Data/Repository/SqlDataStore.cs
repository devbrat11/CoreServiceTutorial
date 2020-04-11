using CoreService.Data.Entities;
using CoreService.Helpers;
using CoreService.Models.BaseDto;
using CoreService.Models.ResultDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreService.Data.Repository
{
    public class SqlDataStore : IDataStore
    {
        private readonly CoreServiceContext _coreServiceContext;

        public SqlDataStore(CoreServiceContext coreServiceContext)
        {
            _coreServiceContext = coreServiceContext;
        }

        public List<UserResultDto> GetUsers()
        {
            var result = new List<UserResultDto>();
            var users = _coreServiceContext.Users.ToList();
            foreach (var user in users)
            {
                var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                var team = _coreServiceContext.Teams.FirstOrDefault(x =>
                    x.Name.Equals(user.Team, StringComparison.InvariantCultureIgnoreCase));
                var assets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(user.Id)).ToList();

                result.Add(new UserResultDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    EmailId = emailId,
                    Team = team.ConvertToTeamDto(),
                    Assets = assets.ConvertToAssetDto()
                });

            }
            return result;
        }

        public UserResultDto GetUser(Guid id)
        {
            var user = _coreServiceContext.Users.FirstOrDefault(x => x.Id.Equals(id));
            if (user != null)
            {
                var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                var team = _coreServiceContext.Teams.FirstOrDefault(x =>
                    x.Name.Equals(user.Team, StringComparison.InvariantCultureIgnoreCase));
                var assets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(user.Id)).ToList();

                return new UserResultDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    EmailId = emailId,
                    Team = team.ConvertToTeamDto(),
                    Assets = assets.ConvertToAssetDto()
                };
            }

            return null;
        }

        public bool TryRegisteringUser(UserEntities userEntity)
        {
            if (!_coreServiceContext.UsersLoginInfo.Any(x => x.EmailId.Equals(userEntity.UserCredentials.EmailId)))
            {
                _coreServiceContext.UsersLoginInfo.Add(userEntity.UserCredentials);
                _coreServiceContext.SaveChanges();
                var userLoginInfo = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.EmailId.Equals(userEntity.UserCredentials.EmailId));

                if (userLoginInfo != null)
                {
                    userEntity.UserDetails.Id = userLoginInfo.Id;
                    _coreServiceContext.Users.Add(userEntity.UserDetails);
                    return true;
                }
            }
            return false;
        }

        public Tuple<bool, Guid> IsUserValid(string emailId, string password)
        {
            var user = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.EmailId.Equals(emailId));
            if (user != null)
            {
                if (user.Password.Equals(password.GetHash()))
                {
                    return new Tuple<bool, Guid>(true, user.Id);
                }
            }
            return new Tuple<bool, Guid>(false, Guid.Empty);
        }

        public List<AssetDto> GetUserAssets(Guid userId)
        {
            var userAssets = new List<AssetDto>();
            var assets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(userId)).ToList();
            foreach (var asset in assets)
            {
                userAssets.Add(asset.ConvertToAssetDto());
            }
            return userAssets;
        }

        public bool TryRegisteringAsset(Asset asset)
        {
            if (_coreServiceContext.Assets.Any(x => x.SerialNumber == asset.SerialNumber))
            {
                return false;
            }

            _coreServiceContext.Assets.Add(asset);
            return true;
        }

        public Tuple<bool, string> TryAllocatingAssetToUser(string assetSerialNumber, Guid userId)
        {
            var asset = _coreServiceContext.Assets.FirstOrDefault(x => x.SerialNumber == assetSerialNumber);
            if (asset != null)
            {
                if (asset.OwnerId != Guid.Empty)
                {
                    var user = GetUser(asset.OwnerId);
                    return new Tuple<bool, string>(false, $"Asset is already allocated to user :\n {JsonConvert.SerializeObject(user)}.");
                }
                asset.OwnerId = userId;
                return new Tuple<bool, string>(true, "Asset allocated to user successfully.");
            }

            return new Tuple<bool, string>(false, "Asset does not exists.");
        }

        public bool TryUpdatingAssetDetails(Asset asset)
        {
            var assetEntity = _coreServiceContext.Assets.FirstOrDefault(x => x.SerialNumber == asset.SerialNumber);
            if (assetEntity != null)
            {
                assetEntity.Brand = asset.Brand;
                assetEntity.ModelNumber = asset.ModelNumber;
                assetEntity.Type = asset.Type;
                assetEntity.HostName = asset.HostName;
            }

            return false;
        }

        public List<AssetOutputDto> GetAllAssets()
        {
            var requiredAssets = new List<AssetOutputDto>();
            var assets = _coreServiceContext.Assets.ToList();
            foreach (var asset in assets)
            {
                UserResultDto owner = null;
                if (asset.OwnerId != Guid.Empty)
                {
                    var user = _coreServiceContext.Users.FirstOrDefault(x => x.Id.Equals(asset.OwnerId));
                    var teamDetails = _coreServiceContext.Teams.FirstOrDefault(x =>
                        x.Name.Equals(user.Team, StringComparison.InvariantCultureIgnoreCase));
                    if (user != null)
                    {
                        var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                        owner = user.AsUserResultDto(emailId);
                        owner.Team = teamDetails.ConvertToTeamDto();
                    }
                }
                requiredAssets.Add(asset.AsAssetOutputDto(owner));

            }
            return requiredAssets;
        }

        public AssetOutputDto GetAsset(string serialNumber)
        {
            var asset = _coreServiceContext.Assets.FirstOrDefault(x => x.SerialNumber.Equals(serialNumber));
            if (asset != null)
            {
                UserResultDto owner = null;
                if (asset.OwnerId != Guid.Empty)
                {
                    var user = _coreServiceContext.Users.FirstOrDefault(x => x.Id.Equals(asset.OwnerId));
                    var teamDetails = _coreServiceContext.Teams.FirstOrDefault(x =>
                        x.Name.Equals(user.Team, StringComparison.InvariantCultureIgnoreCase));
                    if (user != null)
                    {
                        var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                        owner = user.AsUserResultDto(emailId);
                        owner.Team = teamDetails.ConvertToTeamDto();
                    }
                }
                return asset.AsAssetOutputDto(owner);
            }

            return null;
        }

        public bool TryAddingTeam(Team team)
        {
            if (_coreServiceContext.Teams.Any(x => x.Name.Equals(team.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                return false;
            }

            _coreServiceContext.Teams.Add(team);
            return true;
        }

        public List<Team> GetAllTeamsInformation()
        {
            var teams = _coreServiceContext.Teams.ToList();
            return teams;
        }

        public Team GetTeamInformation(string teamName)
        {
            var team = _coreServiceContext.Teams.FirstOrDefault(x => x.Name.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));
            return team;
        }

        public List<AssetOutputDto> GetTeamAssets(string teamName)
        {
            var teamAssets = new List<AssetOutputDto>();
            var users = _coreServiceContext.Users.Where(x =>
                x.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (var user in users)
            {
                var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                var userDto = user.AsUserResultDto(emailId);
                var userAssets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(user.Id));
                foreach (var userAsset in userAssets)
                {
                    teamAssets.Add(userAsset.AsAssetOutputDto(userDto));
                }
            }

            return teamAssets;
        }

        public List<UserResultDto> GetTeamMembers(string teamName)
        {
            var teamUsers = new List<UserResultDto>();
            var users = _coreServiceContext.Users.Where(x =>
                x.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (var user in users)
            {
                var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                var assets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(user.Id)).ToList();
                var userDto = new UserResultDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    EmailId = emailId,
                    Assets = assets.ConvertToAssetDto()
                };
                
                teamUsers.Add(userDto);
            }
            return teamUsers;
        }

        public bool SaveChanges()
        {
            _coreServiceContext.SaveChanges();
            return true;
        }

    }
}
