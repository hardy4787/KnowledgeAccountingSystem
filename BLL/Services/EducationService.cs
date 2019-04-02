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
    public class EducationService : IEducationService
    {
        IUnitOfWork Database { get; set; }
        public EducationService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<EducationDTO> GetEducationByProfileId(string id)
        {
            var education = Database.Educations.GetAll().Where(x => x.ProgrammerId == id);
            return Mapper.Map<IEnumerable<Education>, IEnumerable<EducationDTO>>(education);
        }


        public EducationDTO Get(int id)
        {
            return Mapper.Map < Education, EducationDTO> (Database.Educations.Get(id));
        }
          
        public void Insert(EducationDTO education)
        {
            Database.Educations.Insert(Mapper.Map<EducationDTO, Education > (education));
            Database.Save();
        }

        public void Update(EducationDTO education)
        {
            Database.Educations.Update(Mapper.Map<EducationDTO, Education>(education));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Educations.Delete(id);
            Database.Save();
        }
    }
}
