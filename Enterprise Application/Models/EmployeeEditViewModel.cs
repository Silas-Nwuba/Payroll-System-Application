using Enterprise.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application.Models
{
    public class EmployeeEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Number field is required"), Display(Name = "Employee Number")]
        [RegularExpression(@"^[A-Z]{3,3}[0-6]{3}$")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "first Name is required"), StringLength(100, MinimumLength = 2), Display(Name = "First Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-$]")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name"), StringLength(100)]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "first Name is required"), StringLength(100, MinimumLength = 2), Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-$*")]
        public string LastName { get; set; }
        //public string FullName => FirstName + (string.IsNullOrEmpty(MiddleName) ? "" : ("" + (char?)MiddleName[0] + ".").ToUpper()) + LastName;
        [Required(ErrorMessage = "Gender Field is required")]
        [StringLength(10)]
        public string Gender { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[0-9]{11,11}$", ErrorMessage = "Phone number should be 11 digits")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Date of Birth field is required")]
        [DataType(DataType.DateTime)]
        public DateTime DoB { get; set; }
        [Required(ErrorMessage = "Date Joined field is required")]
        [DataType(DataType.DateTime)]
        public DateTime DateJoined { get; set; }
        [Required(ErrorMessage = "Job Role is required")]
        [StringLength(100)]
        public string Designation { get; set; }
        [Required(ErrorMessage = "NINo field is required")]
        [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$", ErrorMessage = "required at least Two Character and six letter")]
        public string NationalInsuranceNumber { get; set; }
        [Required(ErrorMessage = "Address Field is required")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage = "City field is require")]
        [StringLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Post Code field is require")]
        [StringLength(100)]
        public string PostCode { get; set; }
        [Display(Name = "Photo")]
        public IFormFile ImageUrl { get; set; }
        [Display(Name = "Payment Method")]
        [StringLength(100)]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Union Member")]
        [StringLength(100)]
        public UnionMember UnionMember { get; set; }
        [Display(Name = "Student Loan Payment")]
        [StringLength(100)]
        public StudentLoanPayment StudentLoanPayment { get; set; }
    }
}
