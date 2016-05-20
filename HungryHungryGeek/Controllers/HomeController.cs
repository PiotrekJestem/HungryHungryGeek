using System.Web.Mvc;

namespace HungryHungryGeek.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Simple application for employees to order out food.";

            return View();
        }
    }
}