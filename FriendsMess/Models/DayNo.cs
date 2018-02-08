using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendsMess.Models
{
    public class DayNo
    {

        [Key]
        public int Id { get; set; }

        public int DayNumber { get; set; }


        public int? TotalMeal  { get; set; }


        public int? Expense { get; set; }


        public string ResponsibleMember { get; set; }

        [Required]
        public string UserId { get; set; }
        
    }
}