using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using FriendsMess.Migrations;
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
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public ActionResult Save(MealViewModel mealView)
        {
            if (!ModelState.IsValid)
            {
                mealView.Days = _context.Days.ToList();
                mealView.Members = _context.Members.ToList();
                return View("New", mealView);
            }
            var totalMeal = 0;
            foreach (var meal in mealView.Meals )
            {
                
                meal.DayNoId = mealView.Day;
                _context.Meals.AddOrUpdate(meal);
                if (meal.MealNo != null)
                    totalMeal += meal.MealNo.Value;
            }
            var dayInDb = _context.Days.Single(m => m.Id == mealView.Day);
            if (dayInDb == null)
                return HttpNotFound();
            dayInDb.Expense = mealView.Expense;
            dayInDb.ResponsibleMember = mealView.ResponsibleMem;
            dayInDb.TotalMeal = totalMeal;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}