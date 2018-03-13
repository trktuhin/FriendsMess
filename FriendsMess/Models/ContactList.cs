using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class ContactList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address to contact")]
        public string Location { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Select a picture")]
        public string ImageUrl { get; set; }
    }
}