using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Performance
    {
        public int PerformanceId { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int Rating { get; set; } // 1–5

        public string Remarks { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
