using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repository.Models
{
    public class HungryHungryGeekContext : IdentityDbContext
    {
        public HungryHungryGeekContext()
            : base("DefaultConnection")
        {
        }

        public static HungryHungryGeekContext Create()
        {
            return new HungryHungryGeekContext();
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}