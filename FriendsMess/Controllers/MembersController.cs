using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
            var members = _context.Members.Where(m=>m.UserId==userName).Include(m=>m.Deposits).ToList();
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
            var monthNo = (int)Session["MonthNo"];

            if (!ModelState.IsValid)
            {
                return View("MemberForm", model);
            }
            if (model.Id == 0)
            {
                var member = Mapper.Map<MemberViewModel, Member>(model);
                var deposit = new Deposit()
                {
                    Amount = model.Deposit,
                    MonthNo = monthNo,
                    MemberId = model.Id
                };
                member.UserId = User.Identity.GetUserName();
                _context.Members.Add(member);
                _context.Deposits.Add(deposit);
            }
            else
            {
                var memberInDb = _context.Members.SingleOrDefault(m => m.Id == model.Id);
                if (memberInDb == null)
                    return HttpNotFound();

                var deposit = _context.Deposits.SingleOrDefault(m => m.MonthNo == monthNo && m.MemberId==model.Id);
                if (deposit == null)
                {
                    var newDeposit = new Deposit()
                    {
                        Amount = model.Deposit,
                        MonthNo = monthNo,
                        MemberId = model.Id
                    };
                    _context.Deposits.Add(newDeposit);
                }
                else
                    deposit.Amount = model.Deposit;
                
               
                    
                Mapper.Map(model, memberInDb);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id,int monthNo)
        {
            var memberInDb = _context.Members.Include(m=>m.Deposits).SingleOrDefault(m => m.Id == id);
           
            if (memberInDb == null)
                return HttpNotFound();
            var memDeposit = memberInDb.Deposits.SingleOrDefault(m => m.MonthNo == monthNo);
            
            var monthDeposit = memDeposit == null ? 0 : memberInDb.Deposits.SingleOrDefault(m => m.MonthNo == monthNo).Amount;


            var memberViewModel = new MemberViewModel
            {
                Id = memberInDb.Id,
                Name = memberInDb.Name,
                Deposit =monthDeposit,
                MobileNumber = memberInDb.MobileNumber
            };
            ViewBag.status = "Edit Member";
            return View("MemberForm",memberViewModel);
        }

        public ActionResult Delete(int id)
        {
            var userName = User.Identity.GetUserName();
            var memberInDb = _context.Members.SingleOrDefault(m => m.Id == id);
            
            if (memberInDb == null)
                return HttpNotFound();

            if (userName != memberInDb.UserId)
                return HttpNotFound();

            _context.Members.Remove(memberInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ViewMeal(int id)
        {
            var monthNo = (int) Session["MonthNo"];
            var meals = _context.Meals.Where(m => m.MemberId == id && m.MealNo != 0 && m.DayNoId.Month == monthNo).ToList();
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
            var monthNo = (int) Session["MonthNo"];
            var deposit = _context.Deposits.SingleOrDefault(m => m.MemberId == obj.Id && m.MonthNo==monthNo);
            if(deposit!=null)
                deposit.Amount += obj.Amount;
            else
            {
                var nDeposit = new Deposit()
                {
                    Amount = obj.Amount,
                    MonthNo = monthNo,
                    MemberId = obj.Id
                };
                _context.Deposits.Add(nDeposit);
            }
            //var memberInDb = _context.Members.SingleOrDefault(m => m.Id == obj.Id);
            //memberInDb.Deposit +=obj.Amount;
            _context.SaveChanges();
            return RedirectToAction("Index", "Members");
        }

        public ActionResult AddPicture(int id)
        {
            Session["PictureId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult SaveImage(HttpPostedFileBase file)
        {
            if (file!= null)
            {
                file.SaveAs(Server.MapPath("~/Images/")+file.FileName);
                int memId = (int)Session["PictureId"];
                var memberInDb = _context.Members.SingleOrDefault(m => m.Id == memId);
                if (memberInDb != null)
                {
                    memberInDb.ImagePath =file.FileName;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

        public ActionResult DeletePicture(int id)
        {
            var memberInDb = _context.Members.SingleOrDefault(m => m.Id == id);
            if (memberInDb == null)
                return HttpNotFound();
            memberInDb.ImagePath = "";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}