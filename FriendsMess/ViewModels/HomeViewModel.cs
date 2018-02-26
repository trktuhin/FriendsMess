using System.Collections.Generic;
using FriendsMess.Models;

namespace FriendsMess.ViewModels
{
    public class HomeViewModel
    {
        public IList<Meal> Meals { get; set; }
        public IList<Member> Members { get; set; }
        public IList<DayNo> Days { get; set; }

    }
}