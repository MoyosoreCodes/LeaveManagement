using LeaveManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModels
{
    public class AssignViewModel
    {
        [Display (Name = "Users")]
        public string  UserId { get; set; }

        [Display (Name ="Select Manager ")]
        public string ManagerId { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<ApplicationUser> Managers { get; set; }
    }
}
