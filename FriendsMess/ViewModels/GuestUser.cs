using System.ComponentModel.DataAnnotations;

namespace FriendsMess.ViewModels
{
    public class GuestUser
    {
        [Display(Name = "Enter the username of the admin")]
        public string UserName { get; set; }
    }
}