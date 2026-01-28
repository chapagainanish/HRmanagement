using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Position { get; set; }

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

        [ForeignKey("OrganizationId")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        // initialize collections to avoid null refs
        public ICollection<Attendence> Attendances { get; set; } = new List<Attendence>();
        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
        public ICollection<Performance> Performances { get; set; } = new List<Performance>();
        public ICollection<Recruitment> Recruitments { get; set; } = new List<Recruitment>();
        public ICollection<TravelExpense> TravelExpences { get; set; } = new List<TravelExpense>();
    }
}
