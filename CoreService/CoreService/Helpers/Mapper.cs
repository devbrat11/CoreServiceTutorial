﻿using CoreService.Data.Entities;
using CoreService.Models.BaseDto;
using CoreService.Models.InputDto;
using CoreService.Models.ResultDto;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CoreService.Helpers
{
    public static class CustomMapper
    {
        public static UserEntities ToEntity(this UserRegistrationDto userRegistrationDto)
        {
            var user = new User
            {
                Name = userRegistrationDto.FirstName + " " + userRegistrationDto.LastName,
                DateOfBirth = userRegistrationDto.DateOfBirth,
                Team = userRegistrationDto.Team
            };

            var credentials = new UserCredentials
            {
                EmailId = userRegistrationDto.EmailId,
                Password = GetHash(userRegistrationDto.Password)
            };

            return new UserEntities
            {
                UserDetails = user,
                UserCredentials = credentials
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

        public static UserResultDto AsUserResultDto(this User user, string emailId)
        {
            return new UserResultDto
            {
                Id = user.Id,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                EmailId = emailId
            };
        }

        public static UserResultDto AsUserResultDto(this User user, string emailId, Team team, List<Asset> assets)
        {
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

        public static UserResultDto AsUserResultDto(this User user, string emailId, Team teamDetails)
        {
            return new UserResultDto
            {
                Id = user.Id,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                EmailId = emailId,
                Team = teamDetails.ConvertToTeamDto()
            };
        }

        public static AssetDto AsAssetDto(this Asset asset)
        {
            return new AssetDto
            {
                Type = asset.Type,
                Brand = asset.Brand,
                ModelNumber = asset.ModelNumber,
                SerialNumber = asset.SerialNumber,
                HostName = asset.HostName,
            };
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

        public static List<AssetDto> ConvertToAssetDto(this List<Asset> assets)
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


       

        public static TeamDto ConvertToTeamDto(this Team team)
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
