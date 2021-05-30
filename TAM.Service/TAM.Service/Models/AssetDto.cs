using System;
using TAM.Service.Models.Enum;

namespace TAM.Service.Models
{
    public class AssetDto
    {
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string HostName { get; set; }
        public AssetType Type { get; set; }
        public Guid OwnerId { get; set; }
        public Guid TeamId { get; set; }
    }
}