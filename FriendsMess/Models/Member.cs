using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class Member
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }

        public IList<Deposit> Deposits { get; set; }

        [MinLength(10)]
        public string MobileNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        [Display(Name = "Select Member's Image")]
        public string ImagePath { get; set; }

        public bool IsDeleted { get; set; }
        
    }
}