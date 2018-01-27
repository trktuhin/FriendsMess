using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace FriendsMess.Models.EntityConfiguration
{
    public class MealConfiguration : EntityTypeConfiguration<Meal>
    {
        public MealConfiguration()
        {
            HasKey(m => new {m.DayNoId, m.MemberId});
        }
    }
}