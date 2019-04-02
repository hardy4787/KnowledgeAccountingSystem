using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ProgrammerProfileService : IProgrammerProfileService
    {
        IUnitOfWork Database { get; set; }

        public ProgrammerProfileService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public ProgrammerProfileDTO Get(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            return Mapper.Map<ProgrammerProfile, ProgrammerProfileDTO>(programmer);
        }
        public IEnumerable<ProgrammerProfileDTO> GetBySkill(int id)
        {

            var kek = Database.ProgrammerSkills.GetAll().Where(x => x.SkillId == id).ToList();
            var programmers = kek.Select(y => y.Programmer);

            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(programmers);
        }

        public IEnumerable<ProgrammerProfileDTO> GetAll()
        {
            var programmers = Database.ProgrammerProfiles.GetAll();
            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(programmers);
        }

        public void Update(ProgrammerProfileDTO item)
        {
            Database.ProgrammerProfiles.Update(Mapper.Map<ProgrammerProfileDTO, ProgrammerProfile>(item));
            Database.Save();
        }

        public void UpdateImageProfileUrl(string url, string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            programmer.ImageProfileUrl = url;
            Database.Save();
        }

    }
}
