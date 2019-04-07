using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetProjectsByProfileId(string id);
        void Insert(ProjectDTO education);
        void Update(int id, ProjectDTO education);
        void Delete(int id);
    }
}
