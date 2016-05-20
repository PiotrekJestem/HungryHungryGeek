using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IOrderRepo
    {
        void CreateOrder(Order order);
        IEnumerable<Order> GetByDate(DateTime today);
        void SaveChanges();
    }
}
