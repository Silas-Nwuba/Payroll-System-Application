using Enterprise.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Service
{
   public interface IPayComputeService
    {
        Task CreateAsync(PaymentRecord paymentRecord);
        PaymentRecord GetById(int id);
        TaskYear GetTaxById(int id);
        IEnumerable<PaymentRecord> GetAll();
        IEnumerable<SelectListItem>GetAllTaxYear();
        IEnumerable<SelectListItem> GetAllEmployeesPayRow();
        decimal OverTimeHours(decimal HourWorked, decimal ContratualHour);
        decimal OverTimeRate(decimal HourlyRate);
        decimal OverTimeEarnings(decimal OvertimeRate, decimal OverTimeHour);
        decimal ContratualEarnings(decimal ContratualHour, decimal HourWorked, decimal HourlyRate);
        decimal TotalEarning(decimal OverTimeEarning, decimal ContratualEarning);
        decimal TotalDeduction(decimal tax, decimal Slc, decimal Nic, decimal UnionFee);
        decimal Netpay(decimal TotalEarning, decimal TotalDeduction);
    }
}
