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
        [Display(Name = "Id:")]
        public int Id { get; set; }

        public virtual ICollection<Dish> Dishes { get; private set; }
    }
}
