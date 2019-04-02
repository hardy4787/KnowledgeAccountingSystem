using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ProgrammerProfileDTO, ProgrammerProfile>().ReverseMap();
            CreateMap<ProgrammerSkillDTO, ProgrammerSkill>().ReverseMap();
            CreateMap<SkillDTO, Skill>().ReverseMap();
            CreateMap<ProjectDTO, Project>().ReverseMap();
            CreateMap<EducationDTO, Education>().ReverseMap();
        }
    }
}
