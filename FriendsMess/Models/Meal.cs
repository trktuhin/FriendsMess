namespace FriendsMess.Models
{
    public class Meal
    {
        
        public Member Member { get; set; }
        public int MemberId { get; set; }
        public DayNo DayNo { get; set; }
        public int DayNoId { get; set; }
        public int? MealNo { get; set; }
    }
}