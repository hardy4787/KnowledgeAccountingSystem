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
        IEnumerable<ProjectDTO> GetProjectByProfileId(string id);
        ProjectDTO Get(int id);
        void Insert(ProjectDTO education);
        void Update(ProjectDTO education);
        void Delete(int id);
    }
}
