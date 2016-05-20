using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IHungryHungryGeekContext
    {
        DbSet<Dish> Dishes { get; set; }
        DbSet<Meal> Meals { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Report> Reports { get; set; }

        int SaveChanges();
        Database Database { get; }
    }
}
