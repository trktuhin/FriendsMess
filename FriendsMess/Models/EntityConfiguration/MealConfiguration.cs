using System.Data.Entity.ModelConfiguration;

namespace FriendsMess.Models.EntityConfiguration
{
    public class MealConfiguration : EntityTypeConfiguration<Meal>
    {
        public MealConfiguration()
        {
            HasKey(m => new {m.MemberId,m.DayNoId});
        }
    }
}