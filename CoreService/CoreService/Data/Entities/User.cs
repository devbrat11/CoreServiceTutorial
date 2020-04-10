using System;
using System.ComponentModel.DataAnnotations;

namespace CoreService.Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TeamDetails { get; set; }
    }
}
