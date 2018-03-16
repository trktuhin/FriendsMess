using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendsMess.Models
{
    public class AssignedDate
    {
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime AssignedDay { get; set; }

        public Member Member { get; set; }
        public int MemberId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
    }
}