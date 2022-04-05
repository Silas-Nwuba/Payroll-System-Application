using Enterprise.Entity;
using Enterprise.Service;
using Enterprise_Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RotativaCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application.Controllers
{
    [Authorize(Roles = "Admin , Manager")]
    public class PaymentRecordController : Controller
    {
        private readonly IPayComputeService _paymentRecord;
        private readonly IEnterpriseService _enterpriseService;
        private readonly INationalInsuranceComputation _nationalInsuranceComputation1;
        private readonly ITaxComputation _taxComputation1;
        private decimal OverTimes;
        private decimal OverTimesEarning;
        private decimal ContratualEarning;
        private decimal TotalAmount;
        private decimal union;
        private decimal tax;
        private decimal TotalDeduction;

        public decimal Nlc { get; private set; }
        public decimal SLC { get; private set; }

        public PaymentRecordController(IPayComputeService payComputeService, 
            IEnterpriseService enterpriseService,
            INationalInsuranceComputation nationalInsuranceComputation,
            ITaxComputation taxComputation)
        {
            _paymentRecord = payComputeService;
            _enterpriseService = enterpriseService;
            _nationalInsuranceComputation1 = nationalInsuranceComputation;
            _taxComputation1 = taxComputation;
            
        }
        public IActionResult Index(int? PageNumber)
        {
            var PayMent = _paymentRecord.GetAll().Select(Pay => new PaymentRecordIndexViewModel
            {
                Id = Pay.Id,
                EmployeeId = Pay.EmployeeId,
                FullName = Pay.FullName,
                PayDate = Pay.PaymentDay,
                PayMonth = Pay.PaymentMonth,
                TaxYearId = Pay.TaskYearId,
                Year = _paymentRecord.GetTaxById(Pay.TaskYearId).YearOfTax,
                TotalDeduction = Pay.TotalDeduction,
                TotalEarning = Pay.TotalEarning,
                Employee = Pay.Employee,
                Net = Pay.TotalEarning,
            }).ToList();
            int PageSize = 5;
            return View(PaymentRecordListPagination<PaymentRecordIndexViewModel>.Create(PayMent, PageSize ,PageNumber??1));
        }
        [HttpGet]
     [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
          ViewBag.EmployeeId = _paymentRecord.GetAllEmployeesPayRow();
          ViewBag.TaxYearId = _paymentRecord.GetAllTaxYear(); 
          var CreateView = new PaymentRecordCreateViewModel();
          return View(CreateView);
        }
       
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Create(PaymentRecordCreateViewModel paymentRecord)
        {
            if (ModelState.IsValid)
            {
                var Payment = new PaymentRecord()
                {
                    Id = paymentRecord.Id,
                    EmployeeId = paymentRecord.EmployeeId,
                    FullName = _enterpriseService.GetAsyncById(paymentRecord.EmployeeId).FullName,
                    NiNo = _enterpriseService.GetAsyncById(paymentRecord.EmployeeId).NationalInsuranceNumber,
                    PaymentDay = paymentRecord.PaymentDay,
                    TaskYearId = paymentRecord.TaskYearId,
                    TaskCode = paymentRecord.TaskCode,
                    HourlyWorked = paymentRecord.HourlyWorked,
                    HourlyRate = paymentRecord.HourlyRate,
                    ContratualHour = paymentRecord.ContratualHour,
                    OvertimeEarning = OverTimesEarning = _paymentRecord.OverTimeEarnings(_paymentRecord.OverTimeRate(paymentRecord.HourlyRate), OverTimes),
                    ContratualEarning = ContratualEarning = _paymentRecord.ContratualEarnings(paymentRecord.ContratualHour, paymentRecord.HourlyWorked, paymentRecord.HourlyRate),
                    OvertimeHour = OverTimes = _paymentRecord.OverTimeHours(paymentRecord.HourlyWorked, paymentRecord.ContratualHour),
                    SLc = SLC = _enterpriseService.StudentLoanRefundPayment(paymentRecord.EmployeeId, TotalAmount),
                    NLC = Nlc =  _nationalInsuranceComputation1.NLcAmount(TotalAmount),
                    UnionFee = union = _enterpriseService.UnionFee(paymentRecord.EmployeeId),
                    Tax = tax = _taxComputation1.TaxAmount(TotalAmount),
                    TotalDeduction =TotalDeduction = _paymentRecord.TotalDeduction(tax,SLC,Nlc,union),
                    TotalEarning = TotalAmount = _paymentRecord.TotalEarning(OverTimesEarning, ContratualEarning),
                    NetPayment = _paymentRecord.Netpay(TotalAmount, TotalDeduction),
                };
               await _paymentRecord.CreateAsync(Payment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EmployeeId = _paymentRecord.GetAllEmployeesPayRow();
            ViewBag.TaxYearId = _paymentRecord.GetAllTaxYear();
            return View();
        }
        public IActionResult Details(int id)
        {
            var paymentRecord = _paymentRecord.GetById(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }
            var DetailView = new PaymentDetailViewModel
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                NLC = paymentRecord.NLC,
                PayDate = paymentRecord.PaymentDay,
                TaskYearId = paymentRecord.TaskYearId,
                Year = _paymentRecord.GetTaxById(paymentRecord.TaskYearId).YearOfTax,
                TaskCode = paymentRecord.TaskCode,
                HourlyWorked = paymentRecord.HourlyWorked,
                HourlyRate = paymentRecord.HourlyRate,
                ContratualHour = paymentRecord.ContratualHour,
                TotalDeduction = paymentRecord.TotalDeduction,
                TotalEarning = paymentRecord.TotalEarning,
                NetPayment = paymentRecord.NetPayment,
                PayMonth = paymentRecord.PaymentMonth,
                OvertimeEarning = paymentRecord.OvertimeEarning,
                OvertimeHour = paymentRecord.OvertimeHour,
                OverTimeRate = _paymentRecord.OverTimeRate(paymentRecord.HourlyRate),
                ContratualEarning = paymentRecord.ContratualEarning,
                UnionFee = paymentRecord.UnionFee,
                Tax = paymentRecord.Tax,
                SLc = paymentRecord.SLc,

            };
            return View(DetailView);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult PaySlip(int id)
        {
            var paymentRecord = _paymentRecord.GetById(id);
            if(paymentRecord == null)
            {
                return NotFound();
            }
            var DetailView = new PaymentDetailViewModel
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                NLC = paymentRecord.NLC,
                PayDate = paymentRecord.PaymentDay,
                TaskYearId = paymentRecord.TaskYearId,
                Year = _paymentRecord.GetTaxById(paymentRecord.TaskYearId).YearOfTax,
                TaskCode = paymentRecord.TaskCode,
                HourlyWorked = paymentRecord.HourlyWorked,
                HourlyRate = paymentRecord.HourlyRate,
                ContratualHour = paymentRecord.ContratualHour,
                TotalDeduction = paymentRecord.TotalDeduction,
                TotalEarning = paymentRecord.TotalEarning,
                NetPayment = paymentRecord.NetPayment,
                PayMonth = paymentRecord.PaymentMonth,
                OvertimeEarning = paymentRecord.OvertimeEarning,
                OvertimeHour = paymentRecord.OvertimeHour,
                OverTimeRate = _paymentRecord.OverTimeRate(paymentRecord.HourlyRate),
                ContratualEarning = paymentRecord.ContratualEarning,
                UnionFee = paymentRecord.UnionFee,
                Tax = paymentRecord.Tax,
                SLc = paymentRecord.SLc,

            };
            return View(DetailView);
        }
        public  IActionResult Print(int id)
        {
            var PrintPdf = new ActionAsPdf("PaySlip", new { Id = id })
            {
                FileName = "PaySlip.pdf",

            };
            return PrintPdf;
        }
      
       
    }
}
