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
        [Key]
        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Display(Name = "Name:")]
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
    }
}
