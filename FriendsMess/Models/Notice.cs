using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class Notice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }
        public string UserId { get; set; }
    }
}