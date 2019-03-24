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
        Task<OperationDetails> Create(UserDTO userDto);
        Task<IdentityUser> FindUser(string userName, string password);
        //Task<ClaimsIdentity> Authenticate(string userName, string password);
        
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
