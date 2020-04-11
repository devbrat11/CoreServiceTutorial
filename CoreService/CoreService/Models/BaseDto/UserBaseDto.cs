using System;

namespace CoreService.Models.BaseDto
{
    public class UserBaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
    }
}