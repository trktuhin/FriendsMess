using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class Member
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
        public int Deposit { get; set; }
        [MinLength(10)]
        public string MobileNumber { get; set; }

        [Required]
        public string UserId { get; set; }
        
        
    }
}