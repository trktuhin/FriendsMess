using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class Meal
    {
        
        public Member Member { get; set; }
        public int MemberId { get; set; }
        public DayNo DayNo { get; set; }
        public int DayNoId { get; set; }
        [Display(Name = "Meal number")]
        public int? MealNo { get; set; }
    }
}