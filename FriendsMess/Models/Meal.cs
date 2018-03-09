using System;
using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class Meal
    {
        
        public int MemberId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]  
        public DateTime DayNoId { get; set; }

        [Display(Name = "Meal number")]
        public int? MealNo { get; set; }

        public string UserId { get; set; }

    }
}