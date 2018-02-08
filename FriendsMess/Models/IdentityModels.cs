﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using FriendsMess.Models.EntityConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FriendsMess.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<DayNo> Days { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<OtherExpense> OtherExpenses { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MealConfiguration());

            modelBuilder.Entity<DayNo>()
                .Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            base.OnModelCreating(modelBuilder);
        }
    }
}