using Enterprise.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Service
{
   public interface IEnterpriseService
    {
        //To Create  a new Customer
        Task CreateAsync(Employee Newemployee);
        //To get EmployeebyId 
        Employee GetAsyncById(int employeeId);
        //To Update Employee
        Task UpdateAsync(Employee employee);
        //To Update EmployeebyId
        Task UpdateAsyncByID(int employeeId);
        Task DeleteAsync(int EmployeeId);
        decimal UnionFee(int id);
        decimal StudentLoanRefundPayment(int id, decimal TotalAmount);
        IEnumerable<Employee> GetAll();


    }
}
