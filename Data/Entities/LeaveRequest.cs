using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Data.Entities
{
    public class LeaveRequest
    {   
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public int InitialLeaveBalance { get; set; }
        public int FinalLeaveBalance { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public LeaveType LeaveType { get; set; }
        public ApplicationUser User { get; set; }

    }
}
