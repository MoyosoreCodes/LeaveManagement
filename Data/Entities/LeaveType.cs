using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Data.Entities
{
    public class LeaveType
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
