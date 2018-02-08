﻿using System.Linq;
using System.Web.Mvc;
using FriendsMess.Models;
using FriendsMess.ViewModels;

namespace FriendsMess.Controllers
{
    public class MembersController : Controller
    {
        //
        private ApplicationDbContext _context;

        public MembersController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var members = _context.Members.ToList();
            return View(members);
        }

        public ActionResult New()
        {
            ViewBag.status = "New Member";
            var member=new Member();
            return View("MemberForm",member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberForm", member);
            }
            if (member.Id == 0)
                _context.Members.Add(member);
            else
            {
                var memberInDb = _context.Members.SingleOrDefault(m => m.Id == member.Id);
                if (memberInDb == null)
                    return HttpNotFound();
                memberInDb.Name = member.Name;
                memberInDb.Deposit = member.Deposit;
                memberInDb.MobileNumber = member.MobileNumber;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var memberInDb = _context.Members.SingleOrDefault(m => m.Id == id);
            if (memberInDb == null)
                return HttpNotFound();
            ViewBag.status = "Edit Member";
            return View("MemberForm",memberInDb);
        }

        public ActionResult Delete(int id)
        {
            var memberInDb = _context.Members.SingleOrDefault(m => m.Id == id);
            if (memberInDb == null)
                return HttpNotFound();
            _context.Members.Remove(memberInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewMeal(int id)
        {
            var meals = _context.Meals.Where(m => m.MemberId == id).Where(m=>m.MealNo!=0).ToList();
            var member = _context.Members.SingleOrDefault(m => m.Id == id);
            if (member == null)
                return HttpNotFound();
            var memberName = member.Name;
            ViewBag.member = memberName;
            return View(meals);
        }

        public ActionResult AddDeposit(int id)
        {
            var addDeposit = new AddDeposit
            {
                Id = id,
                Amount = 0
            };
            return View(addDeposit);
        }

        [HttpPost]
        public ActionResult SaveDeposit(AddDeposit obj)
        {
            if (!ModelState.IsValid)
            {
                return View("AddDeposit", obj);
            }
            var memberInDb = _context.Members.SingleOrDefault(m => m.Id == obj.Id);
            memberInDb.Deposit +=obj.Amount;
            _context.SaveChanges();
            return RedirectToAction("Index", "Members");
        }
    }
}