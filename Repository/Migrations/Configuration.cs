using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Models;

namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.HungryHungryGeekContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Models.HungryHungryGeekContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedDishes(context);
        }

        private void SeedRoles(HungryHungryGeekContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole {Name = "Admin"};
                roleManager.Create(role);
            }
        }

        private void SeedUsers(HungryHungryGeekContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new User {UserName = "Admin"};
                var adminresult = manager.Create(user, "password");

                if (adminresult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
        }

        private void SeedDishes(HungryHungryGeekContext context)
        {
            for (int i = 1; i <= 20; i++)
            {
                var dish = new Dish
                {
                    DishId = i,
                    Name = "Delicious meal nr " + i,
                    Description = "Rotten vegetables and meat along with our cook's lack of skills.",
                    Price = i * 1.1
                };
                context.Set<Dish>().AddOrUpdate(dish);
            }
            context.SaveChanges();
        }
    }
}
