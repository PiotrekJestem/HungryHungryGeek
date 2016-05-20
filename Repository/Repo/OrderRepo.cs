using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly HungryHungryGeekContext _db;

        public OrderRepo(HungryHungryGeekContext db)
        {
            this._db = db;
        }

        public void CreateOrder(Order order)
        {
            _db.Orders.Add(order);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Order> GetByDate(DateTime date)
        {
            return _db.Orders.ToList().Where(x => x.OrderDate.Date == date).ToList();
        }
    }
}
