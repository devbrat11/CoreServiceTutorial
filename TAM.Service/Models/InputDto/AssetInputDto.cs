using System;
using System.ComponentModel.DataAnnotations;
using TAMService.Models.BaseDto;

namespace TAMService.Models.InputDto
{
    public class AssetInputDto:AssetDto
    {
        [Required]
        public override string Type { get; set; }
        [Required]
        public override string SerialNumber { get; set; }
        public Guid OwnerId { get; set; }
    }
}