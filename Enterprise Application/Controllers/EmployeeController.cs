using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.Service;
using Enterprise_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_Application.Controllers
{
    public class EmployeeController:Controller
    {
        private readonly IEnterpriseService _enterpriseService;
        public EmployeeController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }
        public IActionResult Index()
        {
            var Employee = _enterpriseService.GetAll().Select(employee => new EmployeeIndexViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                Gender = employee.Gender,
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
            }).ToList();
            return View(Employee);
        }
    }
}
