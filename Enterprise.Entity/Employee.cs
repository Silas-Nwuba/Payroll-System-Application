using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Enterprise.Entity
{
   public class Employee
   {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string MiddleName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
        public DateTime DateJoined { get; set; }
        public string Designation { get; set; }
        [Required]
        [MaxLength(100)]
        public string NationalInsuranceNumber { get; set; }
        [Required]
        [MaxLength(150)]
        public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string PostCode { get; set; }
        public string ImageUrl { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public UnionMember UnionMember { get; set; }
        public StudentLoanPayment StudentLoanPayment { get; set; }
        public IEnumerable<PaymentRecord> PaymentRecords { get; set; }




    }

   
    
}
