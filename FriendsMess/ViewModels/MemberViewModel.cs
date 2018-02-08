using System.ComponentModel.DataAnnotations;

namespace FriendsMess.ViewModels
{
    public class MemberViewModel
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
        public int Deposit { get; set; }
        [MinLength(10)]
        public string MobileNumber { get; set; }
    }
}