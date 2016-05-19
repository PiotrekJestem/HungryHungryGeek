using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class Report
    {
        [Key]
        [Display(Name = "Report Id:")]
        public int ReportId { get; set; }

        [Display(Name = "Report Date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReportDate { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
