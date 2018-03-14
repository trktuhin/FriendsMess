using System;
using System.Security.AccessControl;

namespace FriendsMess.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        public Member Member { get; set; }
        public int MemberId { get; set; }
        public int Amount { get; set; }
        public int MonthNo { get; set; }
        public int YearNo { get; set; }
    }
}