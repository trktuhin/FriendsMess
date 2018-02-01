using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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