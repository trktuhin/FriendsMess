﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;

namespace FriendsMess.Models
{
    public class Member
    {
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
        public int Deposit { get; set; }
        [MinLength(10)]
        public string MobileNumber { get; set; }

        
        
    }
}