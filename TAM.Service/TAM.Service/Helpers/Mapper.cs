using TAMService.Data.Entities;
using TAMService.Models.ResultDto;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TAMService.Models;

namespace TAMService.Helpers
{
    public static class Mapper
    {
        public static User ToEntity(this UserRegistrationInfo userRegistrationDto)
        {
            return new User
            {
                Name = userRegistrationDto.Name,
                DateOfBirth = userRegistrationDto.DateOfBirth,
                Team = userRegistrationDto.Team,
                EmailId = userRegistrationDto.EmailId,
            };
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

        public static UserResultDto AsUserResultDto(this User user, Team team=null, List<Asset> assets=null)
        {
            var resultUser = new UserResultDto
            {
                Id = user.PK,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                EmailId = user.EmailId
            };
            if (team != null)
            {
                resultUser.Team = team.ConvertToTeamDto();
            }

            if (assets != null)
            {
                resultUser.Assets = assets.ConvertToAssetDto();
            }

            return resultUser;
        }

        public static AssetDto ConvertToAssetDto(this Asset asset)
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

        private static List<AssetDto> ConvertToAssetDto(this List<Asset> assets)
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

        private static TeamDto ConvertToTeamDto(this Team team)
        {
            return new TeamDto
            {
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
