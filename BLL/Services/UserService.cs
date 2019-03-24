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

        public async Task<OperationDetails> Create(UserDTO userDto)
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
                ProgrammerProfile programmerProfile = new ProgrammerProfile { Id = user.Id};
                Database.ProgrammerProfiles.Create(programmerProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await Database.UserManager.FindAsync(userName, password);

            return user;
        }
        
        //public async Task<ClaimsIdentity> Authenticate(string userName, string password)
        //{
        //    ClaimsIdentity claim = null;
        //    // находим пользователя
        //    ApplicationUser user = await Database.UserManager.FindAsync(userName, password);
        //    // авторизуем его и возвращаем объект ClaimsIdentity
        //    if (user != null)
        //    {
        //        claim = await Database.UserManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
        //        claim.AddClaim(new Claim("userName", userName));
        //        //claim.AddClaim(new Claim("role", "user"));
        //    }
            
        //    return claim;
        //}

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
