using Enterprise.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application.Models
{
    public class PaymentDetailViewModel
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string NiNo { get; set; }
        [DataType(DataType.Date), Display(Name = "Tax Year")]
        public string Year { get; set; }
        [Display(Name ="Pay Date")]
        public DateTime PayDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Pay Month")]
        public DateTime PayMonth { get; set; } 
        public TaskYear TaskYear { get; set; }
        [Display(Name ="Task Year")]
        public int TaskYearId { get; set; }
        public string TaskCode { get; set; }
        public decimal HourlyRate { get; set; }
        [Display(Name ="Hourly Worked")]
        public decimal HourlyWorked { get; set; }
        [Display(Name = "Contratual Hour")]
        public decimal ContratualHour { get; set; }
        public decimal OvertimeHour { get; set; }
        public decimal OverTimeRate { get; set; }
        public decimal ContratualEarning { get; set; }
        public decimal OvertimeEarning { get; set; }
        public decimal Tax { get; set; }
        public decimal? SLc { get; set; }
        [Display(Name = "Union Fee")]
        public decimal? UnionFee { get; set; }
        public decimal NLC { get; set; }
        [Display(Name = "Total Earning")]
        public decimal TotalEarning { get; set; }
        [Display(Name = "Total Deduction")]
        public decimal TotalDeduction { get; set; }
        [Display(Name ="Net Payment")]
        public decimal NetPayment { get; set; }
    }
}
