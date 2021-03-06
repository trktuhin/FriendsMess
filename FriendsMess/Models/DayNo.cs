﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FriendsMess.Models
{
    public class DayNo
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]  
        public DateTime DayNumber { get; set; }


        public int? TotalMeal  { get; set; }


        public int? Expense { get; set; }


        public string ResponsibleMember { get; set; }

        [Required]
        public string UserId { get; set; }
        
    }
}