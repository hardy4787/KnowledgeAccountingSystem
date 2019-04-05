using BLL.DTO;
using BLL.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> CreateUserAsync(UserDTO userDto);
        Task<IdentityUser> FindUserAsync(string userName, string password);
        Task<OperationDetails> DeleteUser(string userId);
        IList<string> GetRolesByUserId(string id);
    }
}
