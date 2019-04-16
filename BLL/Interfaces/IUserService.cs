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
    /// <summary>
    /// Service for working with user.
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="userDTO">Model of the current user</param>
        /// <returns>Information about result of creating of the new user</returns>
        Task<IdentityOperations> CreateUserAsync(UserDTO userDto);
        /// <summary>
        /// Get user by the current name and password.
        /// </summary>
        /// <param name="userName">User name of the current user</param>
        /// <param name="password">User password of the current user</param>
        /// <returns>User by the user name and password</returns>
        Task<IdentityUser> FindUserAsync(string userName, string password);
        /// <summary>
        /// Deleted user by the current id.
        /// </summary>
        /// <param name="userId">User id of the current user</param>
        /// <returns>User by the user name and password</returns>
        Task<IdentityOperations> DeleteUser(string userId);
        /// <summary>
        /// Get roles by the current user id.
        /// </summary>
        /// <param name="userId">User id of the current user</param>
        /// <returns>Roles of current user</returns>
        IList<string> GetRolesByUserId(string userId);
    }
}
