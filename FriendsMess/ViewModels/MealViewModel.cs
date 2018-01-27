using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FriendsMess.Models;

namespace FriendsMess.ViewModels
{
    public class MealViewModel
    {
        public List<Member> Members { get; set; }
        public List<DayNo> Days { get; set; }
        public List<Meal> Meals { get; set; }
        [Required]
        public int Day { get; set; }
        [GreaterThanZero]
        public int Expense { get; set; }
        public string ResponsibleMem { get; set; }
    }
}