using LeaveManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.ViewModels
{
    public class MakeRequestsViewModel
    {
        [Display (Name = "Select Leave Type")]
        public int LeaveType { get; set; }

        [Display (Name = "Starts:")]
        public DateTime StartDate { get; set; }

        [Display (Name = "Ends:")]
        public DateTime EndDate{ get; set; }

        public IEnumerable<LeaveType> LeaveTypes { get; set; }
    }
}
