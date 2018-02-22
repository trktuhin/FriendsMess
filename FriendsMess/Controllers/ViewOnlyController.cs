using System;
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
            var homeViewModel = new HomeViewModel
            {
                Meals = _context.Meals.ToList(),
                Members = _context.Members.Where(m => m.UserId == UserName).ToList(),
                Days = _context.Days.Where(m => m.Expense != 0 && m.UserId == UserName).ToList()
            };
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
            UserName = (string)Session["GuestUser"];
            var homeViewModel = new HomeViewModel
            {
                Meals = _context.Meals.ToList(),
                Members = _context.Members.Where(m => m.UserId == UserName).ToList(),
                Days = _context.Days.Where(m => m.Expense != 0 && m.UserId == UserName).ToList()
            };
            return View(homeViewModel);
        }

        public ActionResult Expense()
        {
            var days = _context.Days.Where(m => m.Expense != 0 && m.UserId == UserName).OrderByDescending(m => m.DayNumber).ToList();

            return View("Expense", days);
        }


        public int GetTotalMeal(int id)
        {
            var totalMeal = _context.Meals.Where(m => m.MemberId == id).Sum(a => a.MealNo);
            if (totalMeal == null)
                return 0;

            return (int)totalMeal;
        }

        public int GetOtherExpense(string userName)
        {
            try
            {
                var otherExpense = _context.OtherExpenses.Where(m => m.UserId == userName).Sum(c => c.Amount);
                return otherExpense / _context.Members.Count(m => m.UserId == userName);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public float GetMealRate(string userName)
        {

            var totalMeal = _context.Days.Where(m => m.UserId == userName).Sum(m => m.TotalMeal);
            var totalExpense = _context.Days.Where(m => m.UserId == userName).Sum(m => m.Expense);
            try
            {
                return (float)totalExpense / (float)totalMeal;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public ActionResult ExportPdf()
        {
            
            return new ActionAsPdf("Index", "ViewOnly")
            {
                FileName = "Month_Summery.pdf"
            };
        }

        public ActionResult Members()
        {
            var userName =(string)Session["GuestUser"];
            var members = _context.Members.Where(m => m.UserId == userName).ToList();
            return View(members);
        }

        public ActionResult ViewMeal(int id)
        {
            var meals = _context.Meals.Where(m => m.MemberId == id).Where(m => m.MealNo != 0).ToList();
            var member = _context.Members.SingleOrDefault(m => m.Id == id);
            if (member == null)
                return HttpNotFound();
            var memberName = member.Name;
            ViewBag.member = memberName;
            return View(meals);
        }


        public ActionResult Others()
        {
            var userName = (string)Session["GuestUser"];
            var extras = _context.OtherExpenses.Where(m => m.UserId == userName).ToList();
            return View(extras);
        }
    }
}