using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Email { get; set; }
        public int? OrganizationId { get; set; }
        public string? FullName { get; set; }
    }

    public class CreateEmployeeDto
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? EmployeeCode { get; set; }

        public int? OrganizationId { get; set; }

        public DateTime? HireDate { get; set; }

        public decimal? Salary { get; set; }

        public string? Position { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public string? FullName { get; set; }
        [EmailAddress] 
        public string? Email { get; set; }
        public string? EmployeeCode { get; set; }
        public int? OrganizationId { get; set; }
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; }
        public string? Position { get; set; }
    }
}
