using System.Linq;
using System.Web.Mvc;
using FriendsMess.Models;
using FriendsMess.ViewModels;

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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home(GuestUser guest)
        {
            if(guest!=null)
                UserName = guest.UserName;
            var homeViewModel = new HomeViewModel
            {
                Meals = _context.Meals.ToList(),
                Members = _context.Members.Where(m => m.UserId == UserName).ToList(),
                Days = _context.Days.Where(m => m.Expense != 0 && m.UserId == UserName).ToList()
            };
            return View(homeViewModel);
        }

        public int GetTotalMeal(int id)
        {
            var totalMeal = _context.Meals.Where(m => m.MemberId == id).Sum(a => a.MealNo);
            if (totalMeal == null)
                return 0;

            return (int)totalMeal;
        }
    }
}