using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext db) :base(db) { }

        IEnumerable<IdentityRole> IRoleRepository.GetAll(Expression<Func<IdentityRole, bool>>? filter, string? includeProperties = null)
        {
            return base.GetAll(filter, includeProperties);
        }
        IdentityRole IRoleRepository.Get(Expression<Func<IdentityRole, bool>> filter, string? includeProperties, bool isTracked)
        {
            return base.Get(filter, includeProperties, isTracked);
        }
    }
}
