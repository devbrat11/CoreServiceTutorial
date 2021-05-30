using System;
using System.Collections.Generic;

namespace TAM.Service.Models.ResultDto
{
    public class TeamInfo:TeamDto
    {
        public List<UserDetails> Members { get; set; }
        public List<AssetDto> Assets { get; set; }
    }
}