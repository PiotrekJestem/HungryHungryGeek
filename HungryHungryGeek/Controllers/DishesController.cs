using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Repository.Models;

namespace HungryHungryGeek.Controllers
{
    [Authorize]
    public class DishesController : Controller
    {
        private HungryHungryGeekContext db = new HungryHungryGeekContext();

        // GET: Dishes
        public ActionResult Index()
        {
            if(DateTime.Now.Hour >= 12)
                return RedirectToAction("Volunteer");
            return View(db.Dishes.ToList());
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DishId,Name,Description,Price")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Add(dish);
                db.SaveChanges();
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
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DishId,Name,Description,Price")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dish).State = EntityState.Modified;
                db.SaveChanges();
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
            Dish dish = db.Dishes.Find(id);
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
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
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

        public ActionResult Submit()
        {
            var dishes = System.Web.HttpContext.Current.Session["cartContent"] as List<Dish>;
            if (dishes != null)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());
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
                db.Orders.Add(order);
                db.SaveChanges();
                System.Web.HttpContext.Current.Session.Remove("cartContent");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Dishes");
        }

        public ActionResult CompleteOrders()
        {
            if (DateTime.Now.Hour >= 12)
            {
                if (db.Reports.ToList().All(x => x.ReportDate.Date != DateTime.Today))
                {
                    var currentUser = db.Users.Find(User.Identity.GetUserId());
                    var orders = db.Orders.ToList().Where(x => x.OrderDate.Date == DateTime.Today).ToList();
                    if (orders.Any())
                    {
                        db.Reports.Add(new Report {ReportDate = DateTime.Now, User = currentUser});
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Dishes");
        }

        public ActionResult Volunteer()
        {
            if (DateTime.Now.Hour >= 12)
            {
                if (db.Reports.ToList().All(x => x.ReportDate.Date != DateTime.Today))
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
    }
}
