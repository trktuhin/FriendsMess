using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FriendsMess.Models;
using FriendsMess.ViewModels;
using Microsoft.AspNet.Identity;
using Member = FriendsMess.Models.Member;

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
            var yearNo = (int) Session["YearNo"];
            var userName = User.Identity.GetUserName();
            var meals = _context.Meals
                .Where(m=>m.UserId==userName && m.DayNoId.Month==monthNo && m.DayNoId.Year==yearNo)
                .Select(m=>m.MemberId)
                .Distinct()
                .ToList();


            var memList=new List<Member>();

            foreach (var id in meals)
            {
                var mem = _context.Members.Include(m=>m.Deposits).SingleOrDefault(m => m.Id ==id);
                memList.Add(mem);
            }

            var indexView = new IndexViewModel()
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


            return View(indexView);
        }

        public ActionResult Expense()
        {
            var monthNo = (int)Session["MonthNo"];
            var yearNo = (int)Session["YearNo"];
            var userName = User.Identity.GetUserName();
            var days = _context.Days.Where(m => m.Expense != 0 && m.UserId == userName && m.DayNumber.Month == monthNo && m.DayNumber.Year==yearNo).OrderByDescending(m => m.DayNumber).ToList();

            return View("Expense",days);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page";
            var userName = User.Identity.GetUserName();
            var contactLists = _context.ContactLists.Where(m => m.UserId == userName).ToList();

            return View(contactLists);
        }

        public int GetTotalMeal(int id,int monthNo, int yearNo)
        {
            var totalMeal = _context.Meals.Where(m => m.MemberId == id && m.DayNoId.Month == monthNo && m.DayNoId.Year==yearNo).Sum(a => a.MealNo);
            if (totalMeal == null)
                return 0;

            return (int)totalMeal;
        }

        public float GetOtherExpense(string userName,int monthNo,int yearNo)
        {
            try
            {
                var meals = _context.Meals
                    .Where(m => m.UserId == userName && m.DayNoId.Month == monthNo && m.DayNoId.Year == yearNo)
                    .Select(m => m.MemberId)
                    .Distinct()
                    .ToList();
                float otherExpense = _context.OtherExpenses.Where(m => m.UserId == userName && m.MonthNo == monthNo && m.YearNo==yearNo).Sum(c => c.Amount);
                return otherExpense /(float)meals.Count;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public float GetMealRate(string userName,int monthNo,int yearNo)
        {
            var totalMeal = _context.Days.Where(m=>m.UserId==userName && m.DayNumber.Month==monthNo && m.DayNumber.Year==yearNo).Sum(m => m.TotalMeal);
            var totalExpense = _context.Days.Where(m => m.UserId == userName && m.DayNumber.Month == monthNo && m.DayNumber.Year == yearNo).Sum(m => m.Expense);
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
            var yearNo = (int)Session["YearNo"];
            var userName = User.Identity.GetUserName();
            var members = _context.Members.Where(m => m.UserId == userName).Include(m=>m.Deposits).ToList();
            foreach (var mem in members)
            {
                try
                {
                    
                    mem.Deposits.SingleOrDefault(m => m.MonthNo == monthNo && m.YearNo==yearNo).Amount = 0;
                }
                catch (Exception e)
                {
                    Console.Write("Error");
                }
                
            }
            var others = _context.OtherExpenses.Where(m => m.UserId == userName && m.MonthNo==monthNo && m.YearNo==yearNo).ToList();
            foreach (var expense in others)
            {
                _context.OtherExpenses.Remove(expense);
            }
            //var meals = _context.Meals.ToList();
            
            var days = _context.Days.Where(m=>m.UserId==userName && m.DayNumber.Month==monthNo && m.DayNumber.Year==yearNo).ToList();
            foreach (var day in days)
            {
                _context.Days.Remove(day);
            }
            var mealList = _context.Meals.Where(m => m.DayNoId.Month == monthNo && m.DayNoId.Year==yearNo && m.UserId==userName).ToList();
            foreach (var meal in mealList)
            {
                _context.Meals.Remove(meal);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult AddContact()
        {
            ViewBag.status = "Add Contact";
            var contactList=new ContactList();
            return View("ContactForm",contactList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ContactList model)
        {
            if (!ModelState.IsValid)
            {
                return View("ContactForm", model);
            }
            if (model.Id == 0)
            {
                var userName = User.Identity.GetUserName();
                model.UserId =userName;
                _context.ContactLists.Add(model);
            }

            else
            {
                var conInDb = _context.ContactLists.SingleOrDefault(m => m.Id == model.Id);
                if (conInDb == null)
                    return HttpNotFound();
                conInDb.Name = model.Name;
                conInDb.Role = model.Role;
                conInDb.Email = model.Email;
                conInDb.PhoneNumber = model.PhoneNumber;
                conInDb.Location = model.Location;
            }
            _context.SaveChanges();
            return RedirectToAction("Contact");

        }

        public ActionResult DeleteContact(int id)
        {
            var userName = User.Identity.GetUserName();
            var conInDb = _context.ContactLists.SingleOrDefault(m => m.Id == id);
            if (conInDb == null)
                return HttpNotFound();
            if (userName != conInDb.UserId)
                return HttpNotFound();
            _context.ContactLists.Remove(conInDb);
            _context.SaveChanges();
            return RedirectToAction("Contact");
        }

        public ActionResult EditContact(int id)
        {
            ViewBag.status = "Edit Contact";
            var conInDb = _context.ContactLists.SingleOrDefault(m => m.Id == id);
            if (conInDb == null)
                return HttpNotFound();
            return View("ContactForm", conInDb);
        }

        public ActionResult AddPicture(int id)
        {
            Session["PictureId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult SaveImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                int conId = (int)Session["PictureId"];
                var conInDb = _context.ContactLists.SingleOrDefault(m => m.Id == conId);
                if (conInDb != null)
                {
                    conInDb.ImageUrl = file.FileName;
                    _context.SaveChanges();
                    return RedirectToAction("Contact");
                }
            }
            return HttpNotFound();
        }

        public ActionResult DeletePicture(int id)
        {
            var conInDb = _context.ContactLists.SingleOrDefault(m => m.Id == id);
            if (conInDb == null)
                return HttpNotFound();
            conInDb.ImageUrl = "";
            _context.SaveChanges();
            return RedirectToAction("Contact");
        }

        public ActionResult MonthSelector(int month)
        {
            Session["MonthNo"] = month;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult YearSelector(int year)
        {
            Session["YearNo"] = year;

            return RedirectToAction("Index", "Home");
        }

    }
}