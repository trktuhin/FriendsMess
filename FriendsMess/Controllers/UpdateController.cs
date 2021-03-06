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
            var members = _context.Members.Where(m=>m.UserId==userName && m.IsDeleted==false).ToList();

            foreach (var member in members)
            {
                meals.Add(new Meal());
            }


            var mealview = new MealViewModel
            {
                Members = members,
                Meals = meals
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
                var memberCount = _context.Members.Count(m => m.UserId == uName && m.IsDeleted==false);
                if (mealsCount != memberCount)
                {
                    ModelState.AddModelError("", "You must clear search field");
                }
            }


            if (!ModelState.IsValid)
            {
                //mealView.Days = Days();
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
                    Meals = meals
                };
                return View("New", mealview);
            }
           
            var userName = User.Identity.GetUserName();
            var totalMeal = 0;
            foreach (var meal in mealView.Meals )
            {
                
                meal.DayNoId = mealView.Day.Value;
                meal.UserId = User.Identity.GetUserName();

                var mealInDb = _context.Meals.SingleOrDefault(m =>
                    m.UserId == userName && m.DayNoId == meal.DayNoId && m.MemberId == meal.MemberId);
                if (mealInDb == null)
                    _context.Meals.Add(meal);
                else
                {
                    _context.Meals.AddOrUpdate(meal);
                }

                if (meal.MealNo != null)
                    totalMeal += meal.MealNo.Value;
            }
            
            var dayInDb = _context.Days.SingleOrDefault(m => m.DayNumber == mealView.Day && m.UserId==userName);
            if (dayInDb == null)
            {
                var newDay = new DayNo
                {
                    DayNumber = mealView.Day.Value,
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

    }
}