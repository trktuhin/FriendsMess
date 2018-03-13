using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var userName = User.Identity.GetUserName();
            var days = _context.Days.Where(m => m.Expense != 0 && m.UserId == userName && m.DayNumber.Month == monthNo).OrderByDescending(m => m.DayNumber).ToList();

            return View("Expense",days);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page";
            var userName = User.Identity.GetUserName();
            var contactLists = _context.ContactLists.Where(m => m.UserId == userName).ToList();

            return View(contactLists);
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

        //public ActionResult ExportPdf()
        //{
        //    Dictionary<string, string> cookieCollection = new Dictionary<string, string>();

        //    foreach (var key in Request.Cookies.AllKeys)
        //    {
        //        cookieCollection.Add(key, Request.Cookies.Get(key).Value);
        //    }
        //    return new ActionAsPdf("Index","Home")
        //    {
        //        FileName = "Month_Summery.pdf",
        //        Cookies = cookieCollection
        //    };
        //}
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
    }
}