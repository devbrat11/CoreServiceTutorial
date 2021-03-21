using System;
using System.ComponentModel.DataAnnotations;

namespace TAMService.Models.InputDto
{
    public class UserRegistrationDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public string Team { get; set; }
    }
}
