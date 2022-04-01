using Enterprise.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application.Models
{
    public class PaymentRecordCreateViewModel
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
         [Display(Name = "Full Name")]
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string NiNo { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Day")]
        public DateTime PaymentDay { get; set; } = DateTime.UtcNow;
        [DataType(DataType.Date), Display(Name = "Pay Month")]
        public string PaymentMonth { get; set; } = DateTime.Today.Month.ToString();
        public TaskYear TaskYear { get; set; }
        [Display(Name ="Task Year")]
        public int TaskYearId { get; set; }
        public string TaskCode { get; set; } = "2500l";
        public decimal HourlyRate { get; set; }
        [Display(Name ="Hourly Worked")]
        public decimal HourlyWorked { get; set; }
        [Display(Name = "Contratual Hour")]
        public decimal ContratualHour { get; set; } = 144m;
     
        public decimal OvertimeHour { get; set; }
        public decimal ContratualEarning { get; set; }
        public decimal OvertimeEarning { get; set; }
        public decimal Tax { get; set; }
        public decimal? SLc { get; set; }
        public decimal? UnionFee { get; set; }
        public decimal NLC { get; set; }
        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPayment { get; set; }
    }
}
