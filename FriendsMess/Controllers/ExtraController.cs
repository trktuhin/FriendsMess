using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FriendsMess.Models;
using Microsoft.AspNet.Identity;

namespace FriendsMess.Controllers
{
    public class ExtraController : Controller
    {
        //
        // GET: /Extra/
        private ApplicationDbContext _context;

        public ExtraController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            var monthNo = (int) Session["MonthNo"];
            var yearNo = (int) Session["YearNo"];
            var extras = _context.OtherExpenses.Where(m=>m.UserId==userName && m.MonthNo==monthNo && m.YearNo==yearNo).ToList();
            return View(extras);
        }

        public ActionResult New()
        {
            ViewBag.status = "New Expense";
            var expense = new OtherExpense();
            return View("OtherExpense",expense);
        }
        [HttpPost]
        public ActionResult Save(OtherExpense expense)
        {
            var userName = User.Identity.GetUserName();
            var monthNo = (int)Session["MonthNo"];
            var yearNo = (int)Session["YearNo"];

            if (!ModelState.IsValid)
                return View("OtherExpense", expense);
            if (expense.Id == 0)
            {
                expense.UserId = userName;
                expense.MonthNo = monthNo;
                expense.YearNo = yearNo;
                _context.OtherExpenses.Add(expense);
                
            }
            else
            {
                var expenseInDb = _context.OtherExpenses.SingleOrDefault(m => m.Id == expense.Id);
                if (expenseInDb == null)
                    return HttpNotFound();
                expenseInDb.Name = expense.Name;
                expenseInDb.Amount = expense.Amount;
                expenseInDb.Description = expense.Description;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var expenseInDb = _context.OtherExpenses.SingleOrDefault(m => m.Id == id);
            if (expenseInDb == null)
                return HttpNotFound();
            ViewBag.status = "Edit Expense";
            return View("OtherExpense", expenseInDb);
        }

        public ActionResult Delete(int id)
        {
            var expenseInDb = _context.OtherExpenses.SingleOrDefault(m => m.Id == id);
            if (expenseInDb == null)
                return HttpNotFound();
            _context.OtherExpenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowDates()
        {
            var uName = User.Identity.GetUserName();
            var monthNo = (int) Session["MonthNo"];
            var yearNo = (int) Session["YearNo"];
            var days = _context.AssignedDates.Where(m => m.UserId == uName && m.AssignedDay.Month == monthNo && m.AssignedDay.Year==yearNo).Include(m=>m.Member).ToList();
            return View(days);
        }

        public ActionResult DeleteDate(int id)
        {
            var monthNo = (int) Session["MonthNo"];
            var yearNo = (int) Session["YearNo"];
            var uName = User.Identity.GetUserName();
            var dayInDb = _context.AssignedDates.SingleOrDefault(m => m.AssignedDay.Month==monthNo && m.AssignedDay.Day==id && m.AssignedDay.Year==yearNo && m.UserId == uName);
            if (dayInDb == null)
                return HttpNotFound();
            _context.AssignedDates.Remove(dayInDb);
            _context.SaveChanges();
            return RedirectToAction("ShowDates", "Extra");
        }
    }
}