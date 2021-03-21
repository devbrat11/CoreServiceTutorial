using TAMService.Data.Entities;
using TAMService.Models.BaseDto;
using TAMService.Models.InputDto;
using TAMService.Models.ResultDto;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TAMService.Helpers
{
    public static class CustomMapper
    {
        public static User ToEntity(this UserRegistrationDto userRegistrationDto)
        {
            return new User
            {
                Name = userRegistrationDto.FirstName + " " + userRegistrationDto.LastName,
                DateOfBirth = userRegistrationDto.DateOfBirth,
                Team = userRegistrationDto.Team,
                EmailId = userRegistrationDto.EmailId,
                Password = userRegistrationDto.Password,
            };
        }

        public static Asset ToEntity(this AssetInputDto assetInputDto)
        {
            var asset = new Asset
            {
                Brand = assetInputDto.Brand,
                Type = assetInputDto.Type,
                ModelNumber = assetInputDto.ModelNumber,
                SerialNumber = assetInputDto.SerialNumber,
                HostName = assetInputDto.HostName,
                OwnerId = assetInputDto.OwnerId
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
                Id = user.Id,
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

        public static AssetOutputDto AsAssetOutputDto(this Asset asset, UserResultDto owner)
        {
            return new AssetOutputDto
            {
                Type = asset.Type,
                Brand = asset.Brand,
                ModelNumber = asset.ModelNumber,
                SerialNumber = asset.SerialNumber,
                HostName = asset.HostName,
                Owner = owner
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
                    ModelNumber = asset.ModelNumber,
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
