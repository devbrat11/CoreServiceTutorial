using System;
using System.ComponentModel.DataAnnotations;

namespace CoreService.Models
{
    public class AssetDto
    {
        [Required]
        public string Type { get; set; }
        public string ModelNumber { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string HostName { get; set; }
        public Guid OwnerId { get; set; }
    }
}