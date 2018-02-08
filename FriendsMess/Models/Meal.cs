using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class Meal
    {
        


        public int MemberId { get; set; }



        public int DayNoId { get; set; }

        [Display(Name = "Meal number")]
        public int? MealNo { get; set; }
    }
}