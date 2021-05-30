using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TAM.Service.Data.DataStore;
using TAM.Service.Data.Entities;
using TAM.Service.Helpers;

namespace TAM.Service.Data.Repository
{
    public class SqlDataStore : IDataStore
    {
        private readonly TAMServiceContext _context;

        public SqlDataStore(TAMServiceContext coreServiceContext)
        {
            _context = coreServiceContext;
        }

        public List<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User GetUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(x => x.PK.Equals(id));
            return user;
        }

        public bool TryRegisteringUser(User user, LoginDetails userCredential)
        {
            if (!_context.Users.Any(x => x.EmailId.Equals(user.EmailId)))
            {
               if(!_context.UserCredentials.Any(x => x.UserID.Equals(user.EmailId)))
                {
                    _context.Users.Add(user);
                    _context.UserCredentials.Add(userCredential);
                    return true;
                }
            }
            return false;
        }

        public Tuple<bool, Guid> IsUserValid(LoginDetails loginDetails)
        {
            var storedCredential = _context.UserCredentials.FirstOrDefault(x => x.UserID.Equals(loginDetails.UserID));
            if (storedCredential != null)
            {
                if (storedCredential.Password.Equals(loginDetails.Password.GetHash()))
                {
                    var sessionID = Guid.NewGuid();
                    var session =_context.Sessions.FirstOrDefault(x=>x.UserID.Equals(loginDetails.UserID));
                    // creating a session
                    if (session == null)
                    {
                        _context.Sessions.Add(new Session() { UserID = loginDetails.UserID, SessionID = sessionID });
                        _context.SaveChanges();

                        return new Tuple<bool, Guid>(true, sessionID);
                    }
                   
                }
            }
            return new Tuple<bool, Guid>(false, Guid.Empty);
        }

        public List<Asset> GetUserAssets(Guid userId)
        {
            return _context.Assets.Where(x => x.OwnerId.Equals(userId)).ToList();
        }

        public bool TryRegisteringAsset(Asset asset)
        {
            if (_context.Assets.Any(x => x.SerialNumber == asset.SerialNumber))
            {
                return false;
            }

            _context.Assets.Add(asset);
            return true;
        }

        public Tuple<bool, string> TryAllocatingAssetToUser(string assetSerialNumber, Guid userId)
        {
            var asset = _context.Assets.FirstOrDefault(x => x.SerialNumber == assetSerialNumber);
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
            var assetEntity = _context.Assets.FirstOrDefault(x => x.SerialNumber == asset.SerialNumber);
            if (assetEntity != null)
            {
                assetEntity.Brand = asset.Brand;
                assetEntity.Type = asset.Type;
                assetEntity.HostName = asset.HostName;
                assetEntity.TeamId = asset.TeamId;
                assetEntity.OwnerId = asset.OwnerId;
                return true;
            }

            return false;
        }

        public List<Asset> GetAllAssets()
        {
            var assets = _context.Assets.ToList();
            return assets;
        }

        public Asset GetAsset(string serialNumber)
        {
            var asset = _context.Assets.FirstOrDefault(x => x.SerialNumber.Equals(serialNumber));
            return asset;
        }

        public bool TryAddingTeam(Team team)
        {
            if (_context.Teams.Any(x => x.Name.Equals(team.Name)))
            {
                return false;
            }

            _context.Teams.Add(team);
            return true;
        }

        public List<Team> GetAllTeams()
        {
            var teams = _context.Teams.ToList();
            return teams;
        }

        public Team GetTeam(string teamName)
        {
            var team = _context.Teams.FirstOrDefault(x => x.Name.Equals(teamName));
            return team;
        }

        public List<Asset> GetTeamAssets(string teamName)
        {
            var teamAssets = new List<Asset>();
            var users = _context.Users.Where(x =>
                x.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (var user in users)
            {
                var userAssets = _context.Assets.Where(x => x.OwnerId.Equals(user.PK));
                teamAssets.AddRange(userAssets);
            }

            return teamAssets;
        }

        public List<User> GetTeamMembers(string teamName)
        {
            var users = _context.Users.Where(x =>
                x.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return users;
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

    }
}
