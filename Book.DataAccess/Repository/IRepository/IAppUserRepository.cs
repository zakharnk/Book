using Book.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IRepository
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        void Update(AppUser appUser);
        IdentityRole GetUserRole(string appUserId);
        void UpdateUserRole(string appUserId, string roleId);

    }
}
