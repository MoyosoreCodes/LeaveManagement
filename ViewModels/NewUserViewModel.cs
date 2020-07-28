using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.ViewModels
{
    public class NewUserViewModel
    {
        [Display (Name = "Enter Staff ID")]
        public string StaffId { get; set; }

        [Display (Name = "First Name")]
        public string Firstname { get; set; }

        [Display ( Name = "Last Name")]
        public string Lastname { get; set; }
        public string  Email { get; set; }

        [Display (Name = "Assign Role")]
        public string Role { get; set; }
    }
}
