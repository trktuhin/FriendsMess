using System;
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
            var extras = _context.OtherExpenses.Where(m=>m.UserId==userName && m.MonthNo==monthNo).ToList();
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

            if (!ModelState.IsValid)
                return View("OtherExpense", expense);
            if (expense.Id == 0)
            {
                expense.UserId = userName;
                expense.MonthNo = Convert.ToInt32(DateTime.Now.Month.ToString());
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
                expenseInDb.UserId = userName;
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

        public ActionResult AssignMember()
        {
            return View();
        }
    }
}