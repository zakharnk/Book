﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Models.ViewModels
{
    public class UserRoleVM
    {
        [ValidateNever]
        public AppUser AppUser { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string RoleId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }

    }
}
