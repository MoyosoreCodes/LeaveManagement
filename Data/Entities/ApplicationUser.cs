using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="Staff ID")]
        [Required]
        public string  StaffId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ManagerStaffId { get; set; }

        public int LeaveBalance { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string  Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string Lastname { get; set; }

        public string RoleName { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; }

        public string FullName => $"{Firstname} {Lastname}";
    }
}
