using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> CreateUserAsync(UserDTO userDto)
        {
            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.UserName };
                IdentityResult result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ProgrammerProfile programmerProfile = new ProgrammerProfile { Id = user.Id, Email = user.Email, FullName = userDto.FullName };
                Database.ProgrammerProfiles.Insert(programmerProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Registration successfully completed", "");
            }
            else
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
        }

        public IList<string> GetRolesByUserId(string id)
        {
            var roles = Database.UserManager.GetRoles(id);
            return roles;
        }

        public async Task<IdentityUser> FindUserAsync(string userName, string password)
        {
            IdentityUser user = await Database.UserManager.FindAsync(userName, password);
            return user;
        }

        public async Task<OperationDetails> DeleteUser(string userId)
        {
            ApplicationUser user = await Database.UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                Database.ProgrammerProfiles.Delete(userId);
                var logins = user.Logins;
                var rolesForUser = await Database.UserManager.GetRolesAsync(userId);
                foreach (var login in logins.ToList())
                {
                    await Database.UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await Database.UserManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                await Database.UserManager.DeleteAsync(user);
                await Database.SaveAsync();
                return new OperationDetails(true, "User successfully deleted", "user");
            }
            else
                return new OperationDetails(false, "User is not found", "user");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}

