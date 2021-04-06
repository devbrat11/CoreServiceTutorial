﻿using TAMService.Data.Entities;
using TAMService.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TAMService.Data.DataStore;
using TAM.Service.Data.Entities;
using TAMService.Models;

namespace TAMService.Data.Repository
{
    public class SqlDataStore : IDataStore
    {
        private readonly TAMServiceContext _coreServiceContext;

        public SqlDataStore(TAMServiceContext coreServiceContext)
        {
            _coreServiceContext = coreServiceContext;
        }

        public List<User> GetUsers()
        {
            var users = _coreServiceContext.Users.ToList();
            return users;
        }

        public User GetUser(Guid id)
        {
            var user = _coreServiceContext.Users.FirstOrDefault(x => x.Id.Equals(id));
            return user;
        }

        public bool TryRegisteringUser(UserRegistrationDto userRegistrationInfo)
        {
            if (!_coreServiceContext.Users.Any(x => x.EmailId.Equals(userRegistrationInfo.EmailId)))
            {
                var user = userRegistrationInfo.ToEntity();
                var userCredentials = new UserCredential()
                {
                    EmailId = user.EmailId,
                    Password = userRegistrationInfo.Password.GetHash()
                };
                _coreServiceContext.Users.Add(user);
                _coreServiceContext.UserCredentials.Add(userCredentials);
                return true;
            }
            return false;
        }

        public Tuple<bool, Guid> IsUserValid(UserCredential userCredential)
        {
            var userCredentials = _coreServiceContext.UserCredentials.FirstOrDefault(x => x.EmailId.Equals(userCredential.EmailId));
            if (userCredentials != null)
            {
                var user = _coreServiceContext.Users.FirstOrDefault(x => x.EmailId.Equals(userCredential.EmailId));
                if (userCredentials.Password.Equals(userCredential.Password.GetHash()))
                {
                    return new Tuple<bool, Guid>(true, user.Id);
                }
            }
            return new Tuple<bool, Guid>(false, Guid.Empty);
        }

        public List<Asset> GetUserAssets(Guid userId)
        {
            return _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(userId)).ToList();
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
                assetEntity.Type = asset.Type;
                assetEntity.HostName = asset.HostName;
                assetEntity.TeamId = asset.TeamId;
                assetEntity.OwnerId = asset.OwnerId;
            }

            return false;
        }


        public List<Asset> GetAllAssets()
        {
            var assets = _coreServiceContext.Assets.ToList();
            return assets;
        }

        public Asset GetAsset(string serialNumber)
        {
            var asset = _coreServiceContext.Assets.FirstOrDefault(x => x.SerialNumber.Equals(serialNumber));
            return asset;
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

        public List<Asset> GetTeamAssets(string teamName)
        {
            var teamAssets = new List<Asset>();
            var users = _coreServiceContext.Users.Where(x =>
                x.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (var user in users)
            {
                var userAssets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(user.Id));
                teamAssets.AddRange(userAssets);
            }

            return teamAssets;
        }

        public List<User> GetTeamMembers(string teamName)
        {
            var users = _coreServiceContext.Users.Where(x =>
                x.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return users;
        }

        public bool SaveChanges()
        {
            _coreServiceContext.SaveChanges();
            return true;
        }

    }
}