using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TravelExpense
    {
        public int TravelExpenceId { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Purpose { get; set; }

        public decimal Amount { get; set; }

        public DateTime TravelDate { get; set; }

        public string Status { get; set; } // Pending, Approved, Rejected
    }
}
