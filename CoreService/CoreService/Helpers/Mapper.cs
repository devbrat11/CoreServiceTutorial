using System;
using System.Security.Cryptography;
using System.Text;
using CoreService.Data.Entities;
using CoreService.Models;
using Newtonsoft.Json;

namespace CoreService.Helpers
{
    public static class CustomMapper
    {
        public static UserEntities GetUserEntities(this UserRegistrationDto userRegistrationDto)
        {
            var user = new User
            {
                Name = userRegistrationDto.FirstName + " " + userRegistrationDto.LastName,
                DateOfBirth = userRegistrationDto.DateOfBirth,
                TeamDetails = JsonConvert.SerializeObject(userRegistrationDto.TeamDetails)
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

        public static Asset ConvertToEntity(this AssetDto assetDto)
        {
            var asset = new Asset
            {
                Brand = assetDto.Brand,
                Type = assetDto.Type,
                ModelNumber = assetDto.ModelNumber,
                SerialNumber = assetDto.SerialNumber,
                HostName =  assetDto.HostName,
            };

            return asset;
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
