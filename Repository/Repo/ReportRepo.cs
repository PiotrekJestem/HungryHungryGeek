using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class ReportRepo : IReportRepo
    {
        private readonly HungryHungryGeekContext _db;

        public ReportRepo(HungryHungryGeekContext db)
        {
            this._db = db;
        }

        public User GetUserById(string id)
        {
            return _db.Users.Find(id);
        }

        public bool AnyWithDate(DateTime day)
        {
            return _db.Reports.ToList().All(x => x.ReportDate.Date != day);
        }

        public void CreateReport(Report report)
        {
            _db.Reports.Add(report);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
