using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Dish
    {
        public Dish()
        {
            this.Meals = new HashSet<Meal>();
        }

        [Key]
        [Display(Name = "Dish Id:")]
        public int DishId { get; set; }

        [Display(Name = "Dish Name:")]
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Display(Name = "Description:")]
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Display(Name = "Price:")]
        [Required]
        public double Price { get; set; }

        public ICollection<Meal> Meals { get; set; }
    }
}
