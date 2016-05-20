using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class DishRepo : IDishRepo
    {
        private readonly HungryHungryGeekContext _db;

        public DishRepo(HungryHungryGeekContext db)
        {
            _db = db;
        }

        public List<Dish> GetDishes()
        {
            return _db.Dishes.ToList();
        }

        public Dish GetDish(int? id)
        {
            return _db.Dishes.Find(id);
        }

        public void CreateDish(Dish dish)
        {
            _db.Dishes.Add(dish);
            _db.SaveChanges();
        }

        public void UpdateDish(Dish dish)
        {
            _db.Entry(dish).State = EntityState.Modified;
        }

        public void DeleteDish(int id)
        {
            Dish dish = GetDish(id);
            _db.Dishes.Remove(dish);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
