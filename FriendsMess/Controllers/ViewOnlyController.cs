using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FriendsMess.Models;
using FriendsMess.ViewModels;
using Rotativa;

namespace FriendsMess.Controllers
{
    [AllowAnonymous]
    public class ViewOnlyController : Controller
    {
        private ApplicationDbContext _context;
        public static string UserName;
        public static int MonthNo;
        public static int YearNo;

        public ViewOnlyController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult ViewHome()
        {
            UserName = (string)Session["GuestUser"];
            MonthNo = (int)Session["MonthNo"];
            YearNo = (int)Session["YearNo"];

            var meals = _context.Meals
                .Where(m => m.UserId == UserName && m.DayNoId.Month == MonthNo && m.DayNoId.Year == YearNo)
                .Select(m => m.MemberId)
                .Distinct()
                .ToList();


            var memList = new List<Member>();

            foreach (var id in meals)
            {
                var mem = _context.Members.Include(m => m.Deposits).SingleOrDefault(m => m.Id == id);
                memList.Add(mem);
            }
            var homeViewModel = new IndexViewModel()
            {
                Members = memList
            };
            var listOfMonth = new List<SelectListItem>
            {
                new SelectListItem(){ Value = "0", Text = "Select Month", Selected = true},
                new SelectListItem(){ Value = "1", Text = "January"},
                new SelectListItem(){ Value = "2", Text = "February"},
                new SelectListItem(){ Value = "3", Text = "March"},
                new SelectListItem(){ Value = "4", Text = "April"},
                new SelectListItem(){ Value = "5", Text = "May"},
                new SelectListItem(){ Value = "6", Text = "June"},
                new SelectListItem(){ Value = "7", Text = "July"},
                new SelectListItem(){ Value = "8", Text = "August"},
                new SelectListItem(){ Value = "9", Text = "September"},
                new SelectListItem(){ Value = "10", Text = "October"},
                new SelectListItem(){ Value = "11", Text = "November"},
                new SelectListItem(){ Value = "12", Text = "December"}
            };

            ViewBag.list = listOfMonth;

            return View("Home",homeViewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home(GuestUser guest)
        {
            if(guest!=null)
            Session["GuestUser"] = guest.UserName;
            Session["MonthNo"] = DateTime.Now.Month;
            Session["YearNo"] = DateTime.Now.Year;

            UserName = (string)Session["GuestUser"];
            MonthNo = (int) Session["MonthNo"];
            YearNo = (int) Session["YearNo"];

            var meals = _context.Meals
                .Where(m => m.UserId == UserName && m.DayNoId.Month == MonthNo && m.DayNoId.Year == YearNo)
                .Select(m => m.MemberId)
                .Distinct()
                .ToList();


            var memList = new List<Member>();

            foreach (var id in meals)
            {
                var mem = _context.Members.Include(m => m.Deposits).SingleOrDefault(m => m.Id == id);
                memList.Add(mem);
            }

            var homeViewModel = new IndexViewModel()
            {
                Members = memList
            };


            var listOfMonth = new List<SelectListItem>
            {
                new SelectListItem(){ Value = "0", Text = "Select Month", Selected = true},
                new SelectListItem(){ Value = "1", Text = "January"},
                new SelectListItem(){ Value = "2", Text = "February"},
                new SelectListItem(){ Value = "3", Text = "March"},
                new SelectListItem(){ Value = "4", Text = "April"},
                new SelectListItem(){ Value = "5", Text = "May"},
                new SelectListItem(){ Value = "6", Text = "June"},
                new SelectListItem(){ Value = "7", Text = "July"},
                new SelectListItem(){ Value = "8", Text = "August"},
                new SelectListItem(){ Value = "9", Text = "September"},
                new SelectListItem(){ Value = "10", Text = "October"},
                new SelectListItem(){ Value = "11", Text = "November"},
                new SelectListItem(){ Value = "12", Text = "December"}
            };

            ViewBag.list = listOfMonth;
            return View(homeViewModel);
        }

        public ActionResult Expense()
        {
            var days = _context.Days.Where(m => m.Expense != 0 && m.UserId == UserName && m.DayNumber.Month == MonthNo && m.DayNumber.Year == YearNo).OrderByDescending(m => m.DayNumber).ToList();

            return View("Expense", days);
        }


        public int GetTotalMeal(int id, int monthNo, int yearNo)
        {
            var totalMeal = _context.Meals.Where(m => m.MemberId == id && m.DayNoId.Month == monthNo && m.DayNoId.Year == yearNo).Sum(a => a.MealNo);
            if (totalMeal == null)
                return 0;

            return (int)totalMeal;
        }

        public float GetOtherExpense(string userName, int monthNo, int yearNo)
        {
            try
            {
                var meals = _context.Meals
                    .Where(m => m.UserId == userName && m.DayNoId.Month == monthNo && m.DayNoId.Year == yearNo)
                    .Select(m => m.MemberId)
                    .Distinct()
                    .ToList();
                float otherExpense = _context.OtherExpenses.Where(m => m.UserId == userName && m.MonthNo == monthNo && m.YearNo == yearNo).Sum(c => c.Amount);
                return otherExpense/ (float)meals.Count;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public float GetMealRate(string userName, int monthNo, int yearNo)
        {

            var totalMeal = _context.Days.Where(m => m.UserId == userName && m.DayNumber.Month == monthNo && m.DayNumber.Year == yearNo).Sum(m => m.TotalMeal);
            var totalExpense = _context.Days.Where(m => m.UserId == userName && m.DayNumber.Month == monthNo && m.DayNumber.Year == yearNo).Sum(m => m.Expense);
            try
            {
                return (float)totalExpense / (float)totalMeal;
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public ActionResult Members()
        {
            var members = _context.Members.Where(m => m.UserId == UserName && m.IsDeleted == false).Include(m => m.Deposits).ToList();
            return View(members);
        }

        public ActionResult ViewMeal(int id)
        {
            var meals = _context.Meals.Where(m => m.MemberId == id).Where(m => m.MealNo != 0 && m.DayNoId.Month == MonthNo && m.DayNoId.Year == YearNo).ToList();
            var member = _context.Members.SingleOrDefault(m => m.Id == id);
            if (member == null)
                return HttpNotFound();
            var memberName = member.Name;
            ViewBag.member = memberName; 
            return View(meals);
        }


        public ActionResult Others()
        {
            var extras = _context.OtherExpenses.Where(m => m.UserId == UserName && m.MonthNo==MonthNo && m.YearNo==YearNo).ToList();
            return View(extras);
        }

        public ActionResult MonthSelector(int month)
        {
            Session["MonthNo"] = month;

            return RedirectToAction("viewHome", "ViewOnly");
        }

        public ActionResult YearSelector(int year)
        {
            Session["YearNo"] = year;
            return RedirectToAction("viewHome", "ViewOnly");
        }

        public ActionResult Details(int id, int monthNo, int yearNo)
        {
            var memberInDb = _context.Members.Include(m => m.Deposits).SingleOrDefault(m => m.Id == id);

            if (memberInDb == null)
                return HttpNotFound();
            var memDeposit = memberInDb.Deposits.SingleOrDefault(m => m.MonthNo == monthNo && m.YearNo == yearNo);

            var monthDeposit = memDeposit == null ? 0 : memberInDb.Deposits.SingleOrDefault(m => m.MonthNo == monthNo && m.YearNo == yearNo).Amount;


            var memberViewModel = new MemberViewModel
            {
                Id = memberInDb.Id,
                Name = memberInDb.Name,
                Deposit = monthDeposit,
                MobileNumber = memberInDb.MobileNumber,
                ImagePath = memberInDb.ImagePath

            };
            ViewBag.status = "Member Details";
            return View(memberViewModel);
        }

        public ActionResult ShowDates()
        {
            var days = _context.AssignedDates.Where(m => m.UserId == UserName && m.AssignedDay.Month == MonthNo && m.AssignedDay.Year == YearNo).Include(m => m.Member).ToList();
            return View(days);
        }

        public ActionResult Contact()
        {
            var contactLists = _context.ContactLists.Where(m => m.UserId == UserName).ToList();

            return View(contactLists);
        }

        public ActionResult Notice()
        {
            ViewBag.Message = "Your Notices";
            var notices = _context.Notices.Where(m => m.UserId == UserName).ToList();

            return View(notices);
        }
    }
}