using BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Attributes
{
    public class ValidateRoleAttribute : ValidationAttribute
    {
        public IRoleService RoleService
        {
            get { return (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService)); }
        }

        public override bool IsValid(object value)
        {
            int? id = value as int?;
            if (id == null)
                return false;
            var role = RoleService.GetById(id.Value);
            if (role == null)
            {
                return false;
            }
            if (role.Name == "admin")
            {
                return false;
            }
            return true;         
        }
    }
}