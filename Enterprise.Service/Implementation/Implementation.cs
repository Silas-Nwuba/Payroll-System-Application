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
        public Implementation(ApplicationDbContext context)
        {
            _Context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _Context.Employees.ToList();
        }
        public async Task CreateAsync(Employee Newemployee)
        {
            await _Context.Employees.AddAsync(Newemployee);
            await _Context.SaveChangesAsync();
        }
        public Employee GetAsyncById(int EmployeeId)
        {
            var GetId = _Context.Employees.Where(c => c.Id == EmployeeId).FirstOrDefault();
            return GetId;
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
            throw new NotImplementedException();
        }

        public decimal UnionFee(int id)
        {
            throw new NotImplementedException();
        }

       
        public Task UpdateAsyncByID(int employeeId)
        {
            throw new NotImplementedException();
        }

       
    }
}
