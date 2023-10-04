using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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



        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AppUser> appUserList = _unitOfWork.AppUser.GetAll(includeProperties: "Company").ToList();
            foreach (var item in appUserList)
            {
                item.Role = (await userManager.GetRolesAsync(item)).First();
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
