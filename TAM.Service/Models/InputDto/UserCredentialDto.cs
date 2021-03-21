using System.ComponentModel.DataAnnotations;

namespace TAMService.Models.InputDto
{
    public class UserCredentialDto
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}