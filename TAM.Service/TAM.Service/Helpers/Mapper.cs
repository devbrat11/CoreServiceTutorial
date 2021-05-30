using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TAM.Service.Models;
using TAM.Service.Data.Entities;
using TAM.Service.Models.ResultDto;
using System;

namespace TAM.Service.Helpers
{
    public static class Mapper
    {
        public static Tuple<User, LoginDetails> ToEntity(this UserRegistrationInfo userRegistrationInfo)
        {
            return new Tuple<User, LoginDetails>
                (
                new User
                {
                    Name = userRegistrationInfo.Name,
                    DateOfBirth = userRegistrationInfo.DateOfBirth,
                    Team = userRegistrationInfo.Team,
                    EmailId = userRegistrationInfo.EmailId,
                },
                 new LoginDetails()
                 {
                     UserID = userRegistrationInfo.EmailId,
                     Password = GetHash(userRegistrationInfo.Password)
                 }
                );
        }

        public static Asset ToEntity(this AssetDto assetDto)
        {
            var asset = new Asset
            {
                Brand = assetDto.Brand,
                Type = assetDto.Type,
                SerialNumber = assetDto.SerialNumber,
                HostName = assetDto.HostName,
                OwnerId = assetDto.OwnerId,
                TeamId = assetDto.TeamId
            };

            return asset;
        }

        public static Team ToEntity(this TeamDto team)
        {
            return new Team
            {
                Name = team.Name,
                Department = team.Department,
                ParentTeam = team.ParentTeam,
                Manager = team.Manager,
                Lead = team.Lead
            };
        }

        public static UserDetails ToDto(this User user, Team team = null, List<Asset> assets = null)
        {
            var resultUser = new UserDetails
            {
                Id = user.PK,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                EmailId = user.EmailId
            };
            if (team != null)
            {
                resultUser.Team = team.ToDto();
            }

            if (assets != null)
            {
                resultUser.Assets = assets.ToDto();
            }

            return resultUser;
        }

        public static AssetDto ToDto(this Asset asset)
        {
            return new AssetDto
            {
                Type = asset.Type,
                Brand = asset.Brand,
                SerialNumber = asset.SerialNumber,
                HostName = asset.HostName,
                OwnerId = asset.OwnerId,
                TeamId = asset.TeamId
            };
        }

        private static List<AssetDto> ToDto(this List<Asset> assets)
        {
            var convertedAssets = new List<AssetDto>();
            foreach (var asset in assets)
            {
                convertedAssets.Add(new AssetDto
                {
                    Type = asset.Type,
                    Brand = asset.Brand,
                    SerialNumber = asset.SerialNumber,
                    HostName = asset.HostName,
                });
            }

            return convertedAssets;
        }

        private static TeamDto ToDto(this Team team)
        {
            return new TeamDto
            {
                ID = team.ID,
                Name = team.Name,
                Department = team.Department,
                ParentTeam = team.ParentTeam,
                Manager = team.Manager,
                Lead = team.Lead
            };
        }

        public static string GetHash(this string input)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}
