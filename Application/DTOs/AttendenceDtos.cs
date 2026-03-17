using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class AttendenceDto
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public bool IsPresent { get; set; }
    }

  
    public class CreateAttendenceDto
    {
        [Required(ErrorMessage = "EmployeeId is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }

        [Required(ErrorMessage = "IsPresent is required")]
        public bool IsPresent { get; set; }
    }

    public class UpdateAttendenceDto
    {
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public bool? IsPresent { get; set; }
    }
}
