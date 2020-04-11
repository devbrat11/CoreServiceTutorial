using System;
using System.Collections.Generic;
using CoreService.Models.BaseDto;

namespace CoreService.Models.ResultDto
{
    public class UserResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public TeamDto Team { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}