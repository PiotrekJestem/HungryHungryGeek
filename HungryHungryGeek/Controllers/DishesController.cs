using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Repository.IRepo;
using Repository.Models;
using Repository.Repo;

namespace HungryHungryGeek.Controllers
{
    [Authorize]
    public class DishesController : Controller
    {
        private readonly IDishRepo _dishRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IReportRepo _reportRepo;

        // For test purposes only
        public DishesController()
        {
        }

        public DishesController(IDishRepo dishRepo, IOrderRepo orderRepo, IReportRepo reportRepo)
        {
            _dishRepo = dishRepo;
            _orderRepo = orderRepo;
            _reportRepo = reportRepo;
        }

        // GET: Dishes
        public ActionResult Index()
        {
            if (DateTime.Now.Hour >= 12)
                return RedirectToAction("Volunteer");

            var allDishes = _dishRepo.GetDishes();
            return View(allDishes);
        }


        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = _dishRepo.GetDish(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DishId,Name,Description,Price")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishRepo.CreateDish(dish);
                _dishRepo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        // GET: Dishes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = _dishRepo.GetDish(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DishId,Name,Description,Price")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishRepo.UpdateDish(dish);
                _dishRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = _dishRepo.GetDish(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _dishRepo.DeleteDish(id);
            _dishRepo.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = _dishRepo.GetDish(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            List<Dish> dishes = System.Web.HttpContext.Current.Session["cartContent"] as List<Dish>;
            if (dishes == null)
            {
                dishes = new List<Dish>();
            }
            dishes.Add(dish);
            System.Web.HttpContext.Current.Session["cartContent"] = dishes;
            return RedirectToAction("Index");
        }

        public ActionResult Cart()
        {
            if (DateTime.Now.Hour >= 12)
                return RedirectToAction("Volunteer");
            var dishes = System.Web.HttpContext.Current.Session["cartContent"] as List<Dish> ?? new List<Dish>();
            return View(dishes);
        }

        public ActionResult RemoveFromCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dishes = System.Web.HttpContext.Current.Session["cartContent"] as List<Dish> ?? new List<Dish>();
            var itemToRemove = dishes.Find(r => r.DishId == id);
            dishes.Remove(itemToRemove);
            System.Web.HttpContext.Current.Session["cartContent"] = dishes;
            return RedirectToAction("Cart");
        }

        public async Task<ActionResult> Submit()
        {
            var dishes = System.Web.HttpContext.Current.Session["cartContent"] as List<Dish>;
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var currentUser = _reportRepo.GetUserById(User.Identity.GetUserId());
            if (dishes != null)
            {
                var createdMeal = new Meal
                {
                    Dishes = dishes
                };
                var order = new Order
                {
                    MealToOrder = createdMeal,
                    OrderDate = DateTime.Now,
                    User = currentUser
                };
                await userManager.EmailService.SendAsync(new IdentityMessage
                {
                    Destination = currentUser.Email,
                    Subject = "HungryHungryGeek - Your order",
                    Body = prepareMailBodyForSingleOrder(order)
                });
                _orderRepo.CreateOrder(order);
                _orderRepo.SaveChanges();
                System.Web.HttpContext.Current.Session.Remove("cartContent");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Dishes");
        }

        [HttpPost]
        public async Task<ActionResult> CompleteOrders()
        {
            if (DateTime.Now.Hour >= 12)
            {
                if (_reportRepo.AnyWithDate(DateTime.Today))
                {
                    var orders = _orderRepo.GetByDate(DateTime.Today);
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var currentUser = _reportRepo.GetUserById(User.Identity.GetUserId());
                    if (orders.Any())
                    {
                        await userManager.EmailService.SendAsync(new IdentityMessage
                        {
                            Destination = currentUser.Email,
                            Subject = "HungryHungryGeek - Complete order",
                            Body = prepareMailBodyForReport(orders)
                        });
                        _reportRepo.CreateReport(new Report {ReportDate = DateTime.Now, User = currentUser});
                        _reportRepo.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Dishes");
        }

        private string prepareMailBodyForReport(IEnumerable<Order> orders)
        {
            var mailBody = "You've volunteered to complete today's order, here it is:\n\n";

            foreach (var order in orders)
            {
                mailBody += order;
            }
            return mailBody;
        }

        private string prepareMailBodyForSingleOrder(Order order)
        {
            return "You successfully placed an order, here it is:\n\n" + order;
        }

        public ActionResult Volunteer()
        {
            if (DateTime.Now.Hour >= 12)
            {
                if (_reportRepo.AnyWithDate(DateTime.Today))
                {
                    return View(false);
                }
                else
                {
                    return View(true);
                }
            }
            else
            {
                return RedirectToAction("Index", "Dishes");
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
