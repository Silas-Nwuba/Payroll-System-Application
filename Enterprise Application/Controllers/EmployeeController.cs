using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Enterprise.Entity;
using Enterprise.Service;
using Enterprise_Application.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_Application.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEnterpriseService _enterpriseService;
        //[Obsolete]
        private readonly IWebHostEnvironment _hostingWebRoot;
        public EmployeeController(IEnterpriseService enterpriseService, IWebHostEnvironment hostingEnvironment)
        {
            _enterpriseService = enterpriseService;
            _hostingWebRoot = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Employee = _enterpriseService.GetAll().Select(employee => new EmployeeIndexViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Email = employee.Email,
                ImageUrl = employee.ImageUrl,
                DateJoined = employee.DateJoined,
                City = employee.City,
                Designation = employee.Designation,
            }).ToList();
            return View(Employee);
        }
        [HttpGet] //it will us to view the employee form
        ////[Obsolete]
        public IActionResult Create()
        {
            var ViewModel = new EmployeeCreateViewModel();
            return View(ViewModel);
        }

        [ValidateAntiForgeryToken] // provide cross-site-foreigy token                          
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel employeeCreateView)
        {
            try
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
                        Gender = employeeCreateView.Gender,
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
            catch (Exception)
            {

                throw;
            }

        }
        //to get the employee information
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
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
                    Gender = EmpEdit.Gender,
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
            catch (Exception)
            {

                throw;
            }

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            var EmpEdit = _enterpriseService.GetAsyncById(model.Id);
            if (EmpEdit == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                EmpEdit.Id = model.Id;
                EmpEdit.EmployeeNo = model.EmployeeNo;
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
                //await NewMethod(model, EmpEdit);
                await _enterpriseService.UpdateAsync(EmpEdit);
                return RedirectToAction(nameof(Index));
            };
            return View();

        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var EmpDetails = _enterpriseService.GetAsyncById(id);
            if (EmpDetails == null)
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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var EmpDelete = _enterpriseService.GetAsyncById(id);
            if (EmpDelete == null)
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




    //   [HttpPost]
    //   [ValidateAntiForgeryToken]
    //   public async Task<IActionResult> Delete(EmployeeDeleteViewModel employeeDeleteViewModel)
    //   {
    //       await _enterpriseService.DeleteAsync(employeeDeleteViewModel.Id);
    //        return RedirectToAction(nameof(Index));

    //   }
    //}
}

