using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private ApplicationDbContext _db;
        public AppUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IdentityRole GetUserRole(string appUserId)
        {
            var roleId = _db.UserRoles.First(u => u.UserId == appUserId).RoleId;
            return _db.Roles.First(r => r.Id == roleId);
        }


        public void Update(AppUser appUser)
        {
            _db.AppUsers.Update(appUser);
        }

        public void UpdateUserRole(string appUserId, string roleId)
        {
            var oldRole = _db.UserRoles.First(u => u.UserId == appUserId);
            if (oldRole.RoleId == roleId)
            {
                return;
            }

            _db.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleId, UserId = appUserId });
            _db.UserRoles.Remove(oldRole);
        }
    }
}
