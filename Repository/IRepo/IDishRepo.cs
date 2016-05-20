﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IDishRepo
    {
        List<Dish> GetDishes();
        Dish GetDish(int? id);
        void CreateDish(Dish dish);
        void UpdateDish(Dish dish);
        void DeleteDish(int id);
        void SaveChanges();
    }
}
