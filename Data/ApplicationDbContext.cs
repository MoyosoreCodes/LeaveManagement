using System;
using System.Collections.Generic;
using System.Text;
using LeaveManagement.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeaveType> LeaveTypes{ get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
