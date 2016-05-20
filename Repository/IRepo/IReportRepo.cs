using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository.IRepo
{
    public interface IReportRepo
    {
        User GetUserById(string id);
        bool AnyWithDate(DateTime date);
        void CreateReport(Report report);
        void SaveChanges();
    }
}
