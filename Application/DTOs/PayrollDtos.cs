using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class PayrollDto
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime SalaryMonth { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal NetSalary { get; set; }
    }

    public class CreatePayrollDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime SalaryMonth { get; set; }

        [Required]
        public decimal BasicSalary { get; set; }

        public decimal Allowance { get; set; }

        public decimal Deduction { get; set; }
    }

    public class UpdatePayrollDto
    {
        public decimal? BasicSalary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
    }
}
