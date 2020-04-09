using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreService.Models;

namespace CoreService.Data.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TeamDetails { get; set; }
    }

    public class Asset
    {
        public string Type { get; set; }
        public string ModelNumber { get; set; }
        [Key]
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string AllocatedToUserId { get; set; }
    }


}
