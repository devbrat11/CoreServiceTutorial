using System;
using System.Collections.Generic;
using System.Linq;
using CoreService.Data.Entities;
using CoreService.Helpers;
using CoreService.Models;
using Newtonsoft.Json;

namespace CoreService.Data.Repository
{
    public class SqlDataStore:IDataStore
    {
        private readonly CoreServiceContext _coreServiceContext;

        public SqlDataStore(CoreServiceContext coreServiceContext)
        {
            _coreServiceContext = coreServiceContext;
        }

        public List<UserOutputDto> GetUsers()
        {
            var result = new List<UserOutputDto>();
            var users = _coreServiceContext.Users.ToList();
            foreach (var user in users)
            {
                var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                result.Add(new UserOutputDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    EmailId = emailId,
                    TeamDetails = JsonConvert.DeserializeObject<TeamDetails>(user.TeamDetails)
                });

            }
            return result;
        }

        public UserOutputDto GetUser(Guid id)
        {
            var user = _coreServiceContext.Users.FirstOrDefault(x => x.Id.Equals(id));
            if (user != null)
            {
                var emailId = _coreServiceContext.UsersLoginInfo.FirstOrDefault(x => x.Id.Equals(user.Id))?.EmailId;
                return new UserOutputDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    EmailId = emailId,
                    TeamDetails = JsonConvert.DeserializeObject<TeamDetails>(user.TeamDetails)
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
                    return new Tuple<bool, Guid>(true,user.Id);
                }
            }
            return new Tuple<bool, Guid>(false,Guid.Empty);
        }


        public List<Asset> GetUserAssets(Guid userId)
        {
            var assets = _coreServiceContext.Assets.Where(x => x.OwnerId.Equals(userId)).ToList();
            return assets;
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

        public Tuple<bool, string> TryAllocatingAssetToUser(Guid userId, Asset asset)
        {
            var assetEntity = _coreServiceContext.Assets.FirstOrDefault(x => x.SerialNumber == asset.SerialNumber);
            if (assetEntity!=null)
            {
                if (!string.IsNullOrEmpty(assetEntity.OwnerId.ToString())||assetEntity.OwnerId.ToString()!="null")
                {
                    var user = GetUser(assetEntity.OwnerId);
                    return new Tuple<bool, string>(false, $"Asset is already allocated to user :\n {JsonConvert.SerializeObject(user)}.");
                }
                assetEntity.OwnerId = userId;
            }
            else
            {
                asset.OwnerId = userId;
                _coreServiceContext.Assets.Add(asset);
            }
            return new Tuple<bool, string>(true, "Asset allocated to user successfully.");
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

        public bool SaveChanges()
        {
            _coreServiceContext.SaveChanges();
            return true;
        }

    }
}
