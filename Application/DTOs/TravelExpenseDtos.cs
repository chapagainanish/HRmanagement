using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class TravelExpenseDto
    {
        public int TravelExpenseId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Purpose { get; set; }
    }

    public class CreateTravelExpenseDto
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime TravelDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Purpose { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";

        public string? Description { get; set; }
    }

    public class UpdateTravelExpenseDto
    {
        public DateTime? TravelDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Purpose { get; set; }
    }
}
