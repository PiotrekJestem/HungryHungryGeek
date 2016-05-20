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
        [Display(Name = "Order Id:")]
        public int OrderId { get; set; }
        
        [Display(Name = "Order Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }

        public int MealId { get; set; }

        public virtual Meal MealToOrder { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            var orderDetails = "\nOrdered dishes: ";
            double orderCost = 0;
            foreach (var dish in MealToOrder.Dishes)
            {
                orderDetails += dish.Name + ", ";
                orderCost += dish.Price;
            }
            return "\n\n" + User + orderDetails + "Total cost: " + orderCost;
        }
    }
}
