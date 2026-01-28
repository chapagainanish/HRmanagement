using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Organization
    {
        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }

        public string Address { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public DateTime CreatedAt { get; set; }

        // initialize collection
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
