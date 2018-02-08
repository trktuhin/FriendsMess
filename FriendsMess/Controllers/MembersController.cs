using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FriendsMess.Models;
using FriendsMess.ViewModels;
using Microsoft.AspNet.Identity;

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
            var userName = User.Identity.GetUserName();
            var members = _context.Members.Where(m=>m.UserId==userName).ToList();
            return View(members);
        }

        public ActionResult New()
        {
            ViewBag.status = "New Member";
            var member=new MemberViewModel();
            return View("MemberForm",member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MemberForm", model);
            }
            if (model.Id == 0)
            {
                var member = Mapper.Map<MemberViewModel, Member>(model);
                member.UserId = User.Identity.GetUserName();
                _context.Members.Add(member);
            }
            else
            {
                var memberInDb = _context.Members.SingleOrDefault(m => m.Id == model.Id);
                if (memberInDb == null)
                    return HttpNotFound();

                Mapper.Map(model, memberInDb);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var memberInDb = _context.Members.SingleOrDefault(m => m.Id == id);
           
            if (memberInDb == null)
                return HttpNotFound();
            var memberViewModel = new MemberViewModel
            {
                Id = memberInDb.Id,
                Name = memberInDb.Name,
                Deposit = memberInDb.Deposit,
                MobileNumber = memberInDb.MobileNumber
            };
            ViewBag.status = "Edit Member";
            return View("MemberForm",memberViewModel);
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