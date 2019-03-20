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
    public class ProgrammerService : IProgrammerService
    {
        IUnitOfWork Database { get; set; }

        public ProgrammerService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public void Delete(int id)
        {
            Database.Programmers.Delete(id);
            Database.Save();
        }

        public ProgrammerDTO Get(int id)
        {
            var programmer = Database.Programmers.Get(id);
            return Mapper.Map<Programmer, ProgrammerDTO>(programmer);
        }
        public IEnumerable<ProgrammerDTO> GetBySkill(int id)
        {

            var kek = Database.ProgrammerSkills.GetAll().Where(x => x.SkillId == id).ToList();
            var programmers = kek.Select(y => y.Programmer);

            return Mapper.Map<IEnumerable<Programmer>, IEnumerable<ProgrammerDTO>>(programmers);
        }

        public IEnumerable<ProgrammerDTO> GetAll()
        {
            var programmers = Database.Programmers.GetAll();
            return Mapper.Map<IEnumerable<Programmer>, IEnumerable<ProgrammerDTO>>(programmers);
        }

        public void Insert(ProgrammerDTO programmer)
        {
            Database.Programmers.Insert(Mapper.Map<ProgrammerDTO, Programmer>(programmer));
            Database.Save();
        }

        public void Update(ProgrammerDTO item)
        {
            Database.Programmers.Update(Mapper.Map<ProgrammerDTO, Programmer>(item));
            Database.Save();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
