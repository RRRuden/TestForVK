using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APITests.Common
{
    public class ApplicationDbContextFactory
    {
        public static Guid AdminId = Guid.NewGuid();
        public static Guid UserId = Guid.NewGuid();
        public static Guid UserIdForDelete = Guid.NewGuid();
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Users.AddRange(
                new User()
                {
                    CreatedTime = DateTime.Today,
                    Id = AdminId,
                    Login = "Admin",
                    Password = "Password",
                    UserGroupId = 2,
                    UserStateId = 1
                },
                new User()
                {
                    CreatedTime = DateTime.Today,
                    Id = UserId,
                    Login = "User",
                    Password = "Password",
                    UserGroupId = 1,
                    UserStateId = 1
                },
                new User()
                {
                    CreatedTime = DateTime.Today,
                    Id = UserIdForDelete,
                    Login = "UserForDelete",
                    Password = "Password",
                    UserGroupId = 1,
                    UserStateId = 1
                }
            );
            context.SaveChanges();
            return context;
        }
        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
