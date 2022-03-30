using Enterprise.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise_Application.Models
{
    public class EmployeeCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Employee Number field is required"),Display(Name ="Employee Number")]
        [RegularExpression(@"^[A-Z]{3,3}[0-6]{3}$",ErrorMessage ="the first three characters must be Uppercase and 3 digit")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage = "first Name is required"), StringLength(100), Display(Name = "First Name")]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Z][a-zA-Z""\s-]*$", ErrorMessage = "the First Name must be atleast two characterand Maximum of 50")]
        public string FirstName { get; set; }
        [Display(Name ="Middle Name"),StringLength(100)]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "first Name is required"), StringLength(100, MinimumLength = 2),Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z][a-z][a-zA-Z""'\s-]*$",ErrorMessage = "the Last Name must be atleast two characterand Maximum of 50")]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string FullName => FirstName + (string.IsNullOrEmpty(MiddleName)? " " : (" " + (char?)MiddleName[0] + ".").ToUpper()) + LastName;
        [Required(ErrorMessage ="Gender Field is required")]
        [StringLength(10)]
        public string Gender { get; set; }
        [Display(Name ="Phone Number")]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone number should be 11 digits")]
        [StringLength(100)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Date of birth  field is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of birth")]
        public DateTime DoB { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage = "Date Joined field is required")]
        [Display(Name ="Date Joined")]
        [DataType(DataType.Date)]
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        [Required(ErrorMessage ="Job Role is required")]
        [StringLength(100)]
        public string Designation { get; set; }
        [Required(ErrorMessage ="NINo field is required")]
        [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$",ErrorMessage ="invalid national insurance number")]
        [Display(Name ="NI No")]
        public string NationalInsuranceNumber { get; set; }
        [Required(ErrorMessage ="Address Field is required")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage ="City field is require")]
        [StringLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "City Code field is require")]
        [StringLength(100)]
        [RegularExpression("^[0-6]{6}$",ErrorMessage ="Postcode must be atleast 6 digit")]
        public string  PostCode { get; set; }
        [Display(Name ="Photo")]
        public IFormFile ImageUrl { get; set; }
        [Display(Name ="Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "Union Member")]
        public UnionMember UnionMember { get; set; }
        [Display(Name = "Student Loan")]
        public StudentLoanPayment StudentLoanPayment { get; set; }
    }
}
