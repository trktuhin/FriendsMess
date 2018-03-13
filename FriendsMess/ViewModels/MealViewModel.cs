using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FriendsMess.Models;

namespace FriendsMess.ViewModels
{
    public class MealViewModel
    {
        public List<Member> Members { get; set; }

        public IList<Meal> Meals { get; set; }


        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]  
        public DateTime? Day { get; set; }


        [GreaterThanZero]
        public int Expense { get; set; }


        public string ResponsibleMem { get; set; }
    }
}