using Book.DataAccess.Data;
using Book.Models;
using Book.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }
        public void Initialize()
        {
            //apply migrations if not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            //create role if don't exist
            if (!_roleManager.RoleExistsAsync(Constants.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Constants.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constants.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constants.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constants.Role_Admin)).GetAwaiter().GetResult();

                //if not created roles, also create admin
                _userManager.CreateAsync(new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Admin",
                    PhoneNumber = "111111111",
                    StreetAddress = "Nullo str",
                    State = "PL",
                    PostalCode = "31-543",
                    City = "Cracow"
                }, "TesT_1").GetAwaiter().GetResult();

                AppUser user = _db.AppUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(user, Constants.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}