using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payroll
    {
        public int PayrollId { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowance { get; set; }

        public decimal Deduction { get; set; }

        public decimal NetSalary { get; set; }

        public DateTime SalaryMonth { get; set; }
    }
}
