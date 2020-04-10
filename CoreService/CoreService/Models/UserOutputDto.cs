using System;

namespace CoreService.Models
{
    public class UserOutputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public TeamDetails TeamDetails { get; set; }
    }
}