using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Constants.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            UserRoleVM userRoleVM = new()
            {
                AppUser = _unitOfWork.AppUser.Get(u => u.Id == userId, includeProperties:"Company"),
                RoleId = _unitOfWork.AppUser.GetUserRole(userId).Id,
                RoleList = _unitOfWork.Role.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

            };
            userRoleVM.AppUser.Role = _unitOfWork.Role.Get(u => u.Id == userRoleVM.RoleId).ToString();
            return View(userRoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(UserRoleVM userRoleVM)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.AppUser.UpdateUserRole(userRoleVM.AppUser.Id, userRoleVM.RoleId);
                string? roleName = _unitOfWork.Role.Get(u => u.Id == userRoleVM.RoleId).Name;
                var appUser = _unitOfWork.AppUser.Get(u => u.Id == userRoleVM.AppUser.Id);
                if (roleName == Constants.Role_Company)
                {
                    appUser.CompanyId = userRoleVM.AppUser.CompanyId;                    
                }
                else
                {
                    appUser.CompanyId = null;
                }

                _unitOfWork.AppUser.Update(appUser);
                _unitOfWork.Save();
                TempData["success"] = "User role updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                userRoleVM.RoleList = _unitOfWork.Role.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                userRoleVM.CompanyList = _unitOfWork.Company.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(userRoleVM);
            }
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<AppUser> appUserList = _unitOfWork.AppUser.GetAll(includeProperties: "Company").ToList();
            foreach (var appUser in appUserList)
            {
                appUser.Role = _unitOfWork.AppUser.GetUserRole(appUser.Id).Name;
            }

            return Json(new { data = appUserList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var userFromDb = _unitOfWork.AppUser.Get(u => u.Id == id);
            if (userFromDb == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking a user" });

            }

            if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
            {
                //user is locked we need to unlock
                userFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                //lock user
                userFromDb.LockoutEnd = DateTime.Now.AddYears(100);

            }
            _unitOfWork.AppUser.Update(userFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Operation completed successfully" });
        }
        #endregion
    }
}
