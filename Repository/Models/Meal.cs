using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Meal
    {
        public Meal()
        {
            this.Dishes = new HashSet<Dish>();
        }

        [Key]
        [Display(Name = "Meal Id:")]
        public int MealId { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
