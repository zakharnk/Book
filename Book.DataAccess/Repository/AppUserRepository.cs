using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
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
        public void Update(AppUser appUser)
        {
            _db.AppUsers.Update(appUser);
        }
    }
}
