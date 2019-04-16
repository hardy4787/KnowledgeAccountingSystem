using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface creator defines a factory method that returns a service.
    /// </summary>
    public interface IServiceCreator
    {
        /// <summary>
        /// Method that returns user service.
        /// </summary>
        /// <param name="connection">Database connection string</param>
        IUserService CreateUserService(string connection);
    }
}
