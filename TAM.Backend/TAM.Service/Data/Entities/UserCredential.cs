using System.ComponentModel.DataAnnotations;

namespace TAM.Service.Data.Entities
{
    public class UserCredential
    {
        [Key]
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
