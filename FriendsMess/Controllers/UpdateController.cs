﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
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
            var userName = User.Identity.GetUserName();
            var meals = new List<Meal>();
            var members = _context.Members.Where(m=>m.UserId==userName).ToList();

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
            if (mealView.Meals == null)
            {
                ModelState.AddModelError("", "You must clear search field");
            }
            else
            {
                var uName = User.Identity.GetUserName();
                var mealsCount = mealView.Meals.Count;
                var memberCount = _context.Members.Count(m => m.UserId == uName);
                if (mealsCount != memberCount)
                {
                    ModelState.AddModelError("", "You must clear search field");
                }
            }


            if (!ModelState.IsValid)
            {
                mealView.Days = Days();
                mealView.Members = _context.Members.ToList();
                var name = User.Identity.GetUserName();
                var meals = new List<Meal>();
                var members = _context.Members.Where(m => m.UserId == name).ToList();

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
                return View("New", mealview);
            }
           
            var totalMeal = 0;
            foreach (var meal in mealView.Meals )
            {
                
                meal.DayNoId = mealView.Day;
                meal.UserId = User.Identity.GetUserName();
                _context.Meals.AddOrUpdate(meal);
                if (meal.MealNo != null)
                    totalMeal += meal.MealNo.Value;
            }
            var userName = User.Identity.GetUserName();
            var dayInDb = _context.Days.SingleOrDefault(m => m.DayNumber == mealView.Day && m.UserId==userName);
            if (dayInDb == null)
            {
                var newDay = new DayNo
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