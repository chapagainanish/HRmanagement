using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attendence
    {
        public int AttendanceId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        // navigation renamed to singular to represent the single Employee
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan CheckIn { get; set; }

        public TimeSpan CheckOut { get; set; }

        public bool IsPresent { get; set; }

    }
}

