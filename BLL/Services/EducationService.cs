using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EducationService : IEducationService
    {
        IUnitOfWork Database { get; set; }
        public EducationService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<EducationDTO> GetEducationByProfileId(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            if (programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            var education = Database.Educations.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<Education>, IEnumerable<EducationDTO>>(education);
        }
          
        public void Insert(EducationDTO education)
        {
            Database.Educations.Insert(Mapper.Map<EducationDTO, Education > (education));
            Database.Save();
        }

        public void Update(int educationId, EducationDTO educationDTO)
        {
            if (educationId != educationDTO.Id)
                throw new ValidationException("Education's id don't match", "Id");
            var education = Database.Educations.Get(educationDTO.Id);
            if (education == null)
                throw new ValidationException("Education hasn't found", "Id");
            Database.Educations.Update(Mapper.Map<EducationDTO, Education>(educationDTO));
            Database.Save();
        }

        public void Delete(int id)
        {
            var education = Database.Educations.Get(id);
            if (education == null)
                throw new ValidationException("Education hasn't found", "Id");
            Database.Educations.Delete(id);
            Database.Save();
        }
    }
}
