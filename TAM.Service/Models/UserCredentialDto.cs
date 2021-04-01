using System.ComponentModel.DataAnnotations;

namespace TAMService.Models
{
    public class UserCredentialDto
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}