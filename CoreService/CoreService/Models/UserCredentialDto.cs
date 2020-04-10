using System.ComponentModel.DataAnnotations;

namespace CoreService.Models
{
    public class UserCredentialDto
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}