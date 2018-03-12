﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Rotativa;
using FriendsMess.Models;
using FriendsMess.ViewModels;
using Microsoft.AspNet.Identity;

namespace FriendsMess.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
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
            var monthNo = (int) Session["MonthNo"];
            var userName = User.Identity.GetUserName();
            var indexView = new IndexViewModel()
            {
                Members = _context.Members.Where(m=>m.UserId==userName).OrderBy(m=>m.Id).Include(m=>m.Deposits).ToList()
            };
            return View(indexView);
        }

        public ActionResult Expense()
        {
            var monthNo = (int)Session["MonthNo"];
            var userName = User.Identity.GetUserName();
            var days = _context.Days.Where(m => m.Expense != 0 && m.UserId == userName && m.DayNumber.Month == monthNo).OrderByDescending(m => m.DayNumber).ToList();

            return View("Expense",days);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public int GetTotalMeal(int id,int monthNo)
        {
            var totalMeal = _context.Meals.Where(m => m.MemberId == id && m.DayNoId.Month == monthNo).Sum(a => a.MealNo);
            if (totalMeal == null)
                return 0;

            return (int)totalMeal;
        }

        public float GetOtherExpense(string userName,int monthNo)
        {
            try
            {
                float otherExpense = _context.OtherExpenses.Where(m => m.UserId == userName && m.MonthNo == monthNo).Sum(c => c.Amount);
                return otherExpense /(float)_context.Members.Count(m => m.UserId==userName);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public float GetMealRate(string userName,int monthNo)
        {
            var totalMeal = _context.Days.Where(m=>m.UserId==userName && m.DayNumber.Month==monthNo).Sum(m => m.TotalMeal);
            var totalExpense = _context.Days.Where(m => m.UserId == userName && m.DayNumber.Month == monthNo).Sum(m => m.Expense);
            try
            {
                return (float)totalExpense /(float)totalMeal;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public ActionResult NewMonth()
        {
            var monthNo = (int)Session["MonthNo"];
            var userName = User.Identity.GetUserName();
            var members = _context.Members.Where(m => m.UserId == userName).Include(m=>m.Deposits).ToList();
            foreach (var mem in members)
            {
                try
                {
                    
                    mem.Deposits.SingleOrDefault(m => m.MonthNo == monthNo).Amount = 0;
                    var meals = _context.Meals.Where(m => m.MemberId == mem.Id);
                    foreach (var meal in meals)
                    {
                        _context.Meals.Remove(meal);
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error");
                }
                
            }
            var others = _context.OtherExpenses.Where(m => m.UserId == userName && m.MonthNo==monthNo).ToList();
            foreach (var expense in others)
            {
                _context.OtherExpenses.Remove(expense);
            }
            //var meals = _context.Meals.ToList();
            
            var days = _context.Days.Where(m=>m.UserId==userName && m.DayNumber.Month==monthNo).ToList();
            foreach (var day in days)
            {
                day.Expense = 0;
                day.TotalMeal = 0;
                day.ResponsibleMember = null;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ExportPdf()
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();

            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return new ActionAsPdf("Index","Home")
            {
                FileName = "Month_Summery.pdf",
                Cookies = cookieCollection
            };
        }
    }
}