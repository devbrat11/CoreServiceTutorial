using System;
using System.Collections.Generic;

namespace TAM.Service.Models.ResultDto
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public TeamDto Team { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}