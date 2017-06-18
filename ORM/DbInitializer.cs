using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORM.Models;
using System.Web.Helpers;

namespace ORM
{
    public class DbInitializer: CreateDatabaseIfNotExists<EntityModel>
    {
            protected override void Seed(EntityModel db)
            {
                db.Roles.Add(new Role { Id = 1, Name = "admin" });
                db.Roles.Add(new Role { Id = 2, Name = "moder" });
                db.Roles.Add(new Role { Id = 3, Name = "user" });
                db.Profiles.Add(new Profile
                {
                    Id = 1,
                    Name = "Carl"                  
                });
                db.Users.Add(new User
                {
                    Id = 1,
                    Name = "Carl",
                    Email = "piratik45@gmail.com",
                    Password = Crypto.HashPassword("123456"),
                    Account = 0,
                    ProfileId = 1,
                    RoleId = 1
                });
                db.Profiles.Add(new Profile
                {
                    Id = 2,
                    Name = "Jan"
                });
                db.Users.Add(new User
                {
                    Id = 2,
                    Name = "Jan",
                    Email = "meshonim@yandex.ru",
                    Password = Crypto.HashPassword("123456"),
                    Account = 0,
                    ProfileId = 2,
                    RoleId = 2
                });
                db.Profiles.Add(new Profile
                {
                    Id = 3,
                    Name = "Vasya"
                });
                db.Users.Add(new User
                {
                    Id = 3,
                    Name = "Vasya",
                    Email = "piratik45@yandex.ru",
                    Password = Crypto.HashPassword("123456"),
                    Account = 0,
                    ProfileId = 3,
                    RoleId = 3                 
                });
                
                base.Seed(db);
            }
    }
}
