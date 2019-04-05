using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEducationService
    {
        IEnumerable<EducationDTO> GetEducationByProfileId(string id);
        EducationDTO Get(int id);
        void Insert(EducationDTO education);
        void Update(EducationDTO education);
        void Delete(int id);
    }
}
