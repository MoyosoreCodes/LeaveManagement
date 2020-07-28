using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LeaveManagement.Data;
using LeaveManagement.Data.Entities;
using LeaveManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace LeaveManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Requests()
        {
            
            var vm = new MakeRequestsViewModel
            {
                LeaveTypes = _context.LeaveTypes.ToList()
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Requests(MakeRequestsViewModel requests)
        {
            if( ModelState.IsValid)
            {
                var id = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var leaveTypes = _context.LeaveTypes.ToList();
                var leave = leaveTypes.FirstOrDefault(c => c.Id == requests.LeaveType);
                requests.LeaveTypes = leaveTypes;
                
                var balance = user.LeaveBalance;

                int value = user.LeaveBalance - leave.Capacity;

                if (value <= 0)
                {
                    ModelState.AddModelError("", "Cannot make request, out of available balances");
                    //ViewBag.ShowError = true;
                    //return PartialView("_Error");
                    return View(requests);
                }
                //var update = _userManager.Users.Where(b => b.LeaveBalance == balance);
                //
                LeaveRequest leaveRequest = new LeaveRequest
                {
                    UserId = id.ToString(),
                    LeaveType = leave,
                    RequestDate = DateTime.Now,
                    StartDate = requests.StartDate,
                    EndDate = requests.EndDate,
                    Status = "pending",
                    CreatedAt = DateTime.Now,
                    InitialLeaveBalance = balance,
                    FinalLeaveBalance = value
                };

                //ApplicationUser applicationUser = new ApplicationUser
                //{
                //    Id = id,
                //    LeaveBalance = result
                //};
                user.LeaveBalance = value;
                await _context.LeaveRequests.AddAsync(leaveRequest);
               IdentityResult result = await _userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //await _context.Users.Update();
                
            }

            return View(requests);
        }

        public IActionResult _Error()
        {
            return View();
        }
    }
}
