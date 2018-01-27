using System;
using System.Collections.Generic;
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
        public int Day { get; set; }
        public int Expense { get; set; }
    }
}