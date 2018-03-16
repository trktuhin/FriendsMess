using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendsMess.ViewModels
{
    public class AssignViewModel
    {
        [Required]
        public IList<DateTime?> AssignDay { get; set; }

        public int MemberId { get; set; }
    }
}