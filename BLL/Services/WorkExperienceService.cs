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
    public class WorkExperienceService : IWorkExperienceService
    {
        IUnitOfWork Database { get; set; }
        public WorkExperienceService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Delete(int id)
        {
            var workExperience = Database.WorkExperiences.Get(id);
            if (workExperience == null)
                throw new ValidationException("Work experience hasn't found", "Id");
            Database.WorkExperiences.Delete(id);
            Database.Save();
        }

        public IEnumerable<WorkExperienceDTO> GetWorkExperienceByProfileId(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            if (programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            var workExperience = Database.WorkExperiences.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<WorkExperience>, IEnumerable<WorkExperienceDTO>>(workExperience);
        }

        public void Insert(WorkExperienceDTO workExperienceDTO)
        {
            var workExperience = Database.WorkExperiences.Get(workExperienceDTO.Id);
            if(workExperience != null)
                throw new ValidationException("Work experience with this id already exists", "Id");
            Database.WorkExperiences.Insert(Mapper.Map<WorkExperienceDTO, WorkExperience>(workExperienceDTO));
            Database.Save();
        }

        public void Update(int workExperienceId, WorkExperienceDTO workExperienceDTO)
        {
            if (workExperienceId != workExperienceDTO.Id)
                throw new ValidationException("Skill's id don't match", "Id");
            var workExperience = Database.WorkExperiences.Get(workExperienceDTO.Id);
            if (workExperience == null)
                throw new ValidationException("Work experience hasn't found", "Id");
            Database.WorkExperiences.Update(Mapper.Map<WorkExperienceDTO, WorkExperience>(workExperienceDTO));
            Database.Save();
        }
    }
}
