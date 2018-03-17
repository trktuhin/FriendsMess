using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendsMess.Models
{
    public class Deposit
    {
        public Member Member { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MemberId { get; set; }

        public int Amount { get; set; }

        [Key]
        [Column(Order = 2)]
        public int MonthNo { get; set; }

        [Key]
        [Column(Order = 3)]
        public int YearNo { get; set; }
    }
}