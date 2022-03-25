using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.Entity;
using Enterprise.Service;
using Enterprise_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Enterprise_Application.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEnterpriseService _enterpriseService;
        [Obsolete]
        private readonly IHostingEnvironment _hostingWebRoot;

        [Obsolete]
        public EmployeeController(IEnterpriseService enterpriseService, IHostingEnvironment hostingEnvironment)
        {
            _enterpriseService = enterpriseService;
            _hostingWebRoot = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var Employee = _enterpriseService.GetAll().Select(employee => new EmployeeIndexViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                Gender = employee.Gender,
                Email = employee.Email,
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                Designation = employee.Designation,
            }).ToList();
            return View(Employee);
        }
        [HttpGet] //it will us to view the employee form
        public IActionResult Create()
        {
            var ViewModel = new EmployeeCreateViewModel();
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // provide cross-site-foreigy token
        [Obsolete]
        public async Task<IActionResult> Create(EmployeeCreateViewModel employeeCreateView)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = employeeCreateView.Id,
                    EmployeeNo = employeeCreateView.EmployeeNo,
                    FirstName = employeeCreateView.FirstName,
                    MiddleName = employeeCreateView.MiddleName,
                    LastName = employeeCreateView.LastName,
                    FullName = employeeCreateView.FullName,
                    Email = employeeCreateView.Email,
                    DateJoined = employeeCreateView.DateJoined,
                    Designation = employeeCreateView.Designation,
                    DoB = employeeCreateView.DoB,
                    City = employeeCreateView.City,
                    Address = employeeCreateView.Address,
                    PostCode = employeeCreateView.PostCode,
                    PhoneNumber = employeeCreateView.PhoneNumber,
                    PaymentMethod = employeeCreateView.PaymentMethod,
                    UnionMember = employeeCreateView.UnionMember,
                    StudentLoanPayment = employeeCreateView.StudentLoanPayment,
                    NationalInsuranceNumber = employeeCreateView.NationalInsuranceNumber,
                };
                if (employeeCreateView.ImageUrl != null && employeeCreateView.ImageUrl.Length > 0)
                {
                    var UploadDir = @"Image/Employee";
                    var FileName = Path.GetFileNameWithoutExtension(employeeCreateView.ImageUrl.FileName);
                    var FileExtension = Path.GetExtension(employeeCreateView.ImageUrl.FileName);
                    var WebRootPath = _hostingWebRoot.WebRootPath;
                    FileName = DateTime.UtcNow.ToString("yymmdd").ToUpper() + FileName + FileExtension;
                    var path = Path.Combine(WebRootPath, UploadDir, FileName);
                    await employeeCreateView.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + UploadDir + "/" + FileName;
                    await _enterpriseService.CreateAsync(employee);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        //to get the employee information
        public IActionResult Edit(int id)
        {
            var EmpEdit = _enterpriseService.GetAsyncById(id);
            if (EmpEdit == null)
            {
                return NotFound();
            }
            var Employee = new EmployeeEditViewModel
            {
                Id = EmpEdit.Id,
                EmployeeNo = EmpEdit.EmployeeNo,
                FirstName = EmpEdit.FirstName,
                MiddleName = EmpEdit.MiddleName,
                LastName = EmpEdit.LastName,
                Email = EmpEdit.Email,
                DateJoined = EmpEdit.DateJoined,
                Designation = EmpEdit.Designation,
                DoB = EmpEdit.DoB,
                City = EmpEdit.City,
                Address = EmpEdit.Address,
                PostCode = EmpEdit.PostCode,
                PhoneNumber = EmpEdit.PhoneNumber,
                PaymentMethod = EmpEdit.PaymentMethod,
                UnionMember = EmpEdit.UnionMember,
                StudentLoanPayment = EmpEdit.StudentLoanPayment,
                NationalInsuranceNumber = EmpEdit.NationalInsuranceNumber,
            };
            return View(Employee);
        }
        [HttpPost]
        [Obsolete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var EmpEdit = _enterpriseService.GetAsyncById(model.Id);
                if (EmpEdit == null)
                {
                    return NotFound();
                }
                EmpEdit.Id = model.Id;
                EmpEdit.FirstName = model.FirstName;
                EmpEdit.LastName = model.LastName;
                EmpEdit.MiddleName = model.MiddleName;
                EmpEdit.NationalInsuranceNumber = model.NationalInsuranceNumber;
                EmpEdit.Email = model.Email;
                EmpEdit.Gender = model.Gender;
                EmpEdit.Address = model.Address;
                EmpEdit.PostCode = model.PostCode;
                EmpEdit.City = model.City;
                EmpEdit.Designation = model.Designation;
                EmpEdit.DoB = model.DoB;
                EmpEdit.DateJoined = model.DateJoined;
                EmpEdit.UnionMember = model.UnionMember;
                EmpEdit.StudentLoanPayment = model.StudentLoanPayment;
                EmpEdit.PhoneNumber = model.PhoneNumber;

                await NewMethod(model, EmpEdit);
                await _enterpriseService.UpdateAsync(EmpEdit);
                return RedirectToAction(nameof(Index));
            };
            return View();

        }
        [Obsolete]
        private async Task NewMethod(EmployeeEditViewModel model, Employee EmpEdit)
        {
            if (model.ImageUrl != null && model.ImageUrl.Length > 0)
            {
                var UploadDir = @"Image/Employee";
                var FileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                var FileExtension = Path.GetExtension(model.ImageUrl.FileName);
                var WebRootPath = _hostingWebRoot.WebRootPath;
                FileName = DateTime.UtcNow.ToString("yymmdd").ToUpper() + FileName + FileExtension;
                var path = Path.Combine(WebRootPath, UploadDir, FileName);
                await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                EmpEdit.ImageUrl = "/" + UploadDir + "/" + FileName;
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var EmpDetails = _enterpriseService.GetAsyncById(id);
            if(EmpDetails == null)
            {
                return NotFound();
            }
            var ViewModel = new EmployeeDetailsViewModel
            {
                Id = EmpDetails.Id,
                EmployeeNo = EmpDetails.EmployeeNo,
                FullName = EmpDetails.FullName,
                Email = EmpDetails.Email,
                DateJoined = EmpDetails.DateJoined,
                Designation = EmpDetails.Designation,
                DoB = EmpDetails.DoB,
                City = EmpDetails.City,
                Address = EmpDetails.Address,
                PostCode = EmpDetails.PostCode,
                PhoneNumber = EmpDetails.PhoneNumber,
                PaymentMethod = EmpDetails.PaymentMethod,
                UnionMember = EmpDetails.UnionMember,
                StudentLoanPayment = EmpDetails.StudentLoanPayment,
                NationalInsuranceNumber = EmpDetails.NationalInsuranceNumber,
                ImageUrl = EmpDetails.ImageUrl,

            };

            return View(ViewModel);
        }
        public IActionResult Delete(int id)
        {
            var EmpDelete = _enterpriseService.GetAsyncById(id);
           if(EmpDelete == null)
            {
                return NotFound();
            }
            var Model = new EmployeeDeleteViewModel()
            {
                Id = EmpDelete.Id,
                FullName = EmpDelete.FullName,
            };
            return View(Model);
        }
       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Delete(EmployeeDeleteViewModel employeeDeleteViewModel)
       {
           await _enterpriseService.DeleteAsync(employeeDeleteViewModel.Id);
            return RedirectToAction(nameof(Index));

       }
    }
}
  
