using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsMess.Models;
using FriendsMess.ViewModels;

namespace FriendsMess.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Meals = _context.Meals.ToList(),
                Members = _context.Members.ToList(),
                Days = _context.Days.Where(m=>m.Expense!=0).ToList()
            };
            return View(homeViewModel);
        }

        public ActionResult Expense()
        {
            var days = _context.Days.Where(m => m.Expense != 0).ToList();

            return View("Expense",days);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public int GetTotalMeal(int id)
        {
            var totalMeal = _context.Meals.Where(m => m.MemberId == id).Sum(a => a.MealNo);
            if (totalMeal == null)
                return 0;
            return (int)totalMeal;
        }

        public int GetOtherExpense()
        {
            try
            {
                var otherExpense = _context.OtherExpenses.Sum(c => c.Amount);
                return otherExpense / _context.Members.Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public float GetMealRate()
        {
            var totalMeal = _context.Days.Sum(m => m.TotalMeal);
            var totalExpense = _context.Days.Sum(m => m.Expense);
            try
            {
                return (float)(totalExpense / totalMeal);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}