using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrganizationDto
    {
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public int EmployeeCount { get; set; }
    }

    public class CreateOrganizationDto
    {
        [Required]
        public string OrganizationName { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
    }

    public class UpdateOrganizationDto
    {
        public string? OrganizationName { get; set; }
        public string? Address { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
    }
}
