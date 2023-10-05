using Book.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository.IRepository
{
    public interface IRoleRepository 
    {
        IEnumerable<IdentityRole> GetAll(Expression<Func<IdentityRole, bool>>? filter = null, string? includeProperties = null);
        IdentityRole Get(Expression<Func<IdentityRole, bool>> filter, string? includeProperties = null, bool isTracked = false);
    }
}
