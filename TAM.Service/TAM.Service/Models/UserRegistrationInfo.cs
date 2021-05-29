using System;
using System.ComponentModel.DataAnnotations;

namespace TAMService.Models
{
    public class UserRegistrationInfo
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public string Team { get; set; }
    }
}
