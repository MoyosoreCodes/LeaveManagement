using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LeaveManagement.Data;
using LeaveManagement.Data.Entities;
using LeaveManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LeaveManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewRequests()
        {
            var id = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
            var requests = _context.LeaveRequests
                            .Include(x => x.User).Where(x => x.User.ManagerStaffId == id.ToString())
                            .Include(x => x.LeaveType)
                            .ToList();

            return View(requests);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(NewUserViewModel newuser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Firstname = newuser.Firstname,
                    Lastname = newuser.Lastname,
                    StaffId = newuser.StaffId,
                    UserName = newuser.Email,
                    Email = newuser.Email,
                    CreatedAt = DateTime.Now,
                    LeaveBalance = 20, 
                    RoleName = newuser.Role                    
                };

                var result = await _userManager.CreateAsync(user, "password");

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }


            }

            return View(newuser);
        }

        [HttpGet]
        public IActionResult Assign()
        {
            var users = _context.Users.ToList();
            var managers = users.Where(x => x.RoleName == "Manager" || x.RoleName == "Admin");
            var AssignUsers = users.Where(x => x.RoleName == "Staff");
            var vm = new AssignViewModel
            {
                Managers = managers,
                Users = AssignUsers
            };
                
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(AssignViewModel assign)
        {
            if (ModelState.IsValid)
            {
                // get user based on user id
                var user = await _userManager.FindByIdAsync(assign.UserId);
                user.ManagerStaffId = assign.ManagerId;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    return View("Index");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", "An Error Occured");
                }


         }
            return View(assign);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int RequestId)
        {
                var leaveRequests = _context.LeaveRequests.ToList();
                var reqid = leaveRequests.FirstOrDefault(x => x.Id == RequestId);

                reqid.Status = "Accepted";

                _context.LeaveRequests.Update(reqid);
                await _context.SaveChangesAsync();
            

            return RedirectToAction("ViewRequests");
        }


        [HttpPost]
        public async Task<IActionResult> Reject(int RequestId)
        {
                var leaveRequests = _context.LeaveRequests.ToList();
                var reqid = leaveRequests.FirstOrDefault(x => x.Id == RequestId);

                reqid.Status = "Rejected";

                _context.LeaveRequests.Update(reqid);
            await _context.SaveChangesAsync();


            return RedirectToAction("ViewRequests");
        }
    }
}
