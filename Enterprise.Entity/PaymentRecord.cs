using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise.Entity
{
    public class PaymentRecord
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string NiNo { get; set; }
        public DateTime PaymentDay { get; set; }
        public DateTime PaymentMonth { get; set; } 
        public TaskYear TaskYear { get; set; }

        [ForeignKey("TaskYearId")]
        public int TaskYearId { get; set; }
        public string TaskCode { get; set; }
        [Column(TypeName = "Money")]
        public decimal HourlyRate { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal HourlyWorked { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal ContratualHour { get; set; }
        [Column(TypeName ="Decimal(18,2)")]
        public decimal OvertimeHour { get; set; }
        [Column(TypeName = "Money")]
        public decimal ContratualEarning { get; set;}
        [Column(TypeName = "Money")]
        public decimal OvertimeEarning { get; set; }
        [Column(TypeName = "Money")]
        public decimal Tax { get; set; }
        [Column(TypeName = "Money")]
        public decimal? SLc { get; set; }
        [Column(TypeName = "Money")]
        public decimal? UnionFee { get; set; }
        [Column(TypeName = "Money")]
        public decimal NLC { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalEarning { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalDeduction { get; set; }
        [Column(TypeName ="Money")]
        public decimal NetPayment { get; set; }

    }
}