using Enterprise.Entity;
using Enterprise.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Service.Implementation
{
    public class PayComputeImplementation : IPayComputeService
    {
        private readonly ApplicationDbContext _Context;
        private decimal overTimes;
        private decimal Contract;

        public PayComputeImplementation(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _Context.PaymentRecords.AddAsync(paymentRecord);
            await _Context.SaveChangesAsync();
        }
        public IEnumerable<PaymentRecord> GetAll() => _Context.PaymentRecords.AsNoTracking().OrderBy(p => p.EmployeeId);

        public PaymentRecord GetById(int id) => _Context.PaymentRecords.Where(c => c.Id == id).FirstOrDefault();

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var Tax = _Context.TaskYears.Select(taxyear => new SelectListItem
            {
                Text = taxyear.YearOfTax,
                Value = taxyear.Id.ToString()
            });
            return Tax;
        }
        public decimal OverTimeHours(decimal HourWorked, decimal ContratualHour)
        {
            if (HourWorked <= ContratualHour)
            {
                overTimes = 0.00m;
            }
            else if (HourWorked > ContratualHour)
            {
                overTimes = HourWorked * ContratualHour;
            }
            else if (HourWorked == ContratualHour)
            {
                overTimes = HourWorked * ContratualHour;
            }
            return overTimes;
        }
        public decimal OverTimeRate(decimal HourlyRate) => HourlyRate * 1.5m;
        public decimal OverTimeEarnings(decimal OvertimeRate, decimal OverTimeHour) => OvertimeRate * OverTimeHour;

        public decimal ContratualEarnings(decimal ContratualHour, decimal HourWorked, decimal HourlyRate)
        {
            if (HourWorked > ContratualHour)
            {
                Contract = HourWorked * HourlyRate;
            }
            else
            {
                Contract = ContratualHour * HourlyRate;
            }
            return Contract;
        }
        public decimal TotalDeduction(decimal tax, decimal Slc, decimal Nic, decimal UnionFee)
        => tax + Slc + Nic + UnionFee;
        public decimal TotalEarning(decimal OverTimeEarning, decimal ContratualEarning)
        => OverTimeEarning + ContratualEarning;
        public decimal Netpay(decimal TotalEarning, decimal TotalDeduction)
        => TotalEarning - TotalDeduction;

        public TaskYear GetTaxById(int id) => _Context.TaskYears.Where(task => task.Id == id).FirstOrDefault();

        public IEnumerable<SelectListItem> GetAllEmployeesPayRow()
        {
            var GetEmployee = _Context.Employees.Select(emp => new SelectListItem
            {
                Text = emp.FullName,
                Value = emp.Id.ToString(),
            });
            return GetEmployee;
        }
    }
    
}
