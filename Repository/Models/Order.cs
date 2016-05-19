using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "Id:")]
        public int Id { get; set; }
        
        [Display(Name = "Order Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        public int MealId { get; set; }

        public virtual Meal MealToOrder { get; set; }

        public virtual User User { get; set; }
    }
}
