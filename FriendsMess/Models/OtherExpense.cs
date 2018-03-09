using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class OtherExpense
    {
        public int Id { get; set; }
        [GreaterThanZero]
        public int Amount { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }

        public int MonthNo { get; set; }

        public string UserId { get; set; }
    }
}