using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsMess.Models;
using FriendsMess.ViewModels;

namespace FriendsMess.Controllers
{
    public class UpdateController : Controller
    {
        private ApplicationDbContext _context;

        public UpdateController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var meals = new List<Meal>();
            var members = _context.Members.ToList();

            foreach (var member in members)
            {
                meals.Add(new Meal());
            }

            var mealview = new MealViewModel
            {
                Members = members,
                Meals = meals,
                Days = _context.Days.ToList(),
            };
            return View(mealview);
        }

        //
        // GET: /Update/
        public ActionResult Index()
        {
            return Content("welcome");
        }
        
        [HttpPost]
        public ActionResult Save(IList<Meal> meals,int day,int expense)
        {
            foreach (var meal in meals )
            {
                
                meal.DayNoId = day;
                _context.Meals.AddOrUpdate(meal);
            }
            var dayInDb = _context.Days.Single(m => m.Id == day);
            if (dayInDb == null)
                return HttpNotFound();
            dayInDb.Expense = expense;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}