using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using FriendsMess.Migrations;
using FriendsMess.Models;
using FriendsMess.ViewModels;
using Microsoft.AspNet.Identity;

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
                Days = Days()
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
                mealView.Days = Days();
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
            var userName = User.Identity.GetUserName();
            var dayInDb = _context.Days.SingleOrDefault(m => m.DayNumber == mealView.Day && m.UserId==userName);
            if (dayInDb == null)
            {
                var newDay = new DayNo()
                {
                    DayNumber = mealView.Day,
                    Expense = mealView.Expense,
                    TotalMeal = totalMeal,
                    UserId = userName,
                    ResponsibleMember = mealView.ResponsibleMem
                };
                try
                {
                    _context.Days.Add(newDay);
                    _context.SaveChanges();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                }
                return RedirectToAction("Index");
            }
            dayInDb.Expense = mealView.Expense;
            dayInDb.ResponsibleMember = mealView.ResponsibleMem;
            dayInDb.TotalMeal = totalMeal;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> Days()
        {
            var list = new List<SelectListItem>();

            for (var i = 1; i <= 31; i++)
            {
                list.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            return list;
        }
    }
}