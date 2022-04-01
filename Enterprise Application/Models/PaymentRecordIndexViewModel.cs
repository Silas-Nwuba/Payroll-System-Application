using Enterprise.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application.Models
{
    public class PaymentRecordIndexViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Pay Day")]
        public DateTime PayDate { get; set; }
        [Display(Name = "Month")]
        public DateTime PayMonth { get; set; }
        public string Year { get; set; }
        public int TaxYearId { get; set; }
        [Display(Name = "Total Earnings")]
        public decimal TotalEarning { get; set; }
        [Display(Name = "Total Deduction")]
        public decimal TotalDeduction { get; set; }
        [Display(Name ="Net")]
        public decimal Net { get; set; }
    }
}
