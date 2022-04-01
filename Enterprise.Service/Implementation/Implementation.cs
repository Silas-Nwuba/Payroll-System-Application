using Enterprise.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Persistence;
using System.Linq;

namespace Enterprise.Service.Implementation
{
    public class Implementation : IEnterpriseService
    {
        private readonly ApplicationDbContext _Context;
        private decimal StudentLoan;
        private decimal Fee;

        public Implementation(ApplicationDbContext context)
        {
            _Context = context;
        }
        public IEnumerable<Employee> GetAll() => _Context.Employees;
        public async Task CreateAsync(Employee Newemployee)
        {
            await _Context.Employees.AddAsync(Newemployee);
            await _Context.SaveChangesAsync();
        }
        public Employee GetAsyncById(int EmployeeId)=> _Context.Employees.Where(c => c.Id == EmployeeId).FirstOrDefault();
        //{
        //    var GetId = _Context.Employees.Where(c => c.Id == EmployeeId).FirstOrDefault();
        //    return GetId;
        //}
        public async Task UpdateAsyncByID(int employeeId)
        {
            var Update = _Context.Employees.Where(c => c.Id == employeeId).FirstOrDefault();
            _Context.Employees.Update(Update);
          await _Context.SaveChangesAsync();

        }
        public async Task UpdateAsync(Employee employee)
        {
            _Context.Employees.Update(employee);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int EmployeeId)
        {
            var EmpId = GetAsyncById(EmployeeId);
            _Context.Employees.Remove(EmpId);
            await _Context.SaveChangesAsync();
        }
        public decimal StudentLoanRefundPayment(int id, decimal TotalAmount)
        {
            var Employee = GetAsyncById(id);
            if (Employee.StudentLoanPayment == StudentLoanPayment.Yes && TotalAmount > 1750 && TotalAmount < 2000)
            {
                StudentLoan = .15m;
            }
            else if(Employee.StudentLoanPayment == StudentLoanPayment.Yes && TotalAmount >= 2000 & TotalAmount > 2250)
            {
                StudentLoan = .38m;
            }
            else if(Employee.StudentLoanPayment == StudentLoanPayment.Yes && TotalAmount >=2250 && TotalAmount > 2500)
            {
                StudentLoan = .60m;
            }
            else if (Employee.StudentLoanPayment == StudentLoanPayment.Yes && TotalAmount >= 2250)
            {
                StudentLoan = .83m;
            }
            else
            {
                StudentLoan = .0m;
            }
            return StudentLoan;
        }

        public decimal UnionFee(int id)
        {
            var Employee = GetAsyncById(id);
            if(Employee.UnionMember == UnionMember.Yes)
            {
                Fee = .10m;
            }
            else
            {
                Fee = .0m;
            }
            return Fee;
        }
    }
}
