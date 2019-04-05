using AutoMapper;
using BLL.DTO;
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
            Database.WorkExperiences.Delete(id);
            Database.Save();
        }

        public WorkExperienceDTO Get(int id)
        {
            return Mapper.Map<WorkExperience, WorkExperienceDTO>(Database.WorkExperiences.Get(id));
        }

        public IEnumerable<WorkExperienceDTO> GetWorkExperienceByProfileId(string id)
        {
            var workExperience = Database.WorkExperiences.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<WorkExperience>, IEnumerable<WorkExperienceDTO>>(workExperience);
        }

        public void Insert(WorkExperienceDTO workExperience)
        {
            Database.WorkExperiences.Insert(Mapper.Map<WorkExperienceDTO, WorkExperience>(workExperience));
            Database.Save();
        }

        public void Update(WorkExperienceDTO workExperience)
        {
            Database.WorkExperiences.Update(Mapper.Map<WorkExperienceDTO, WorkExperience>(workExperience));
            Database.Save();
        }
    }
}
