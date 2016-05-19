using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HungryHungryGeek.Models
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
    }
}