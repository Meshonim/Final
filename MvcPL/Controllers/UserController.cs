using BLL.Interfaces.Services;
using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPL.Infrastructure.Mappers;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        private readonly IRoleService roleService;

        public UserController(IUserService userService, IRoleService roleService, IProfileService profileService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.profileService = profileService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var usersAndRoles = new Dictionary<int, string>();
            var adminRoleId = roleService.GetOneByPredicate(r => r.Name == "admin").Id;
            var moderRoleId = roleService.GetOneByPredicate(r => r.Name == "moder").Id;
            var userRoleId = roleService.GetOneByPredicate(r => r.Name == "user").Id;
            usersAndRoles.Add(moderRoleId, "moder");
            usersAndRoles.Add(userRoleId, "user");
            var users = userService.GetAllByPredicate(u => u.RoleId != adminRoleId);
            var model = new ListUserViewModel();
            model.Users = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = user.ToUserViewModel();
                string role = null;
                usersAndRoles.TryGetValue(user.RoleId, out role);
                if (role == null)
                {
                    role = string.Empty;
                }
                userViewModel.Role = role;
                model.Users.Add(userViewModel);
            }
            ViewBag.users = model;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            var user = userService.GetById(id.Value);
            var profile = profileService.GetById(user.ProfileId);         
            userService.Delete(user);
            profileService.Delete(profile);
            return RedirectToAction("Index", "User");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Update(int? id)
        {
            var user = userService.GetById(id.Value);
            var model = new UpdateUserModel();
            model.Email = user.Email;
            model.Name = user.Name;
            var roles = roleService.GetAllByPredicate(r => r.Name != "admin").ToList();
            var list = new List<SelectListItem>();
            foreach (var role in roles)
            {
                list.Add(new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Id.ToString()
                    });
            }
            var selectList = new SelectList(list, "Value", "Text", 1);
            ViewBag.selectList = selectList;
            model.RoleId = user.RoleId;
            ViewBag.id = id;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Update(UpdateUserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = userService.GetById(model.Id);
            var profile = profileService.GetOneByPredicate(p => p.Name == user.Name);
            user.Name = model.Name;
            user.Email = model.Email;
            user.RoleId = model.RoleId;
            userService.Update(user);           
            profile.Name = model.Name;
            profileService.Update(profile);
            return RedirectToAction("Index", "User");
        }
    }
}