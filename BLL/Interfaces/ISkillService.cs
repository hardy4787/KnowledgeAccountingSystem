using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// Service for working with skill.
    /// </summary>
    public interface ISkillService
    {
        /// <summary>
        /// Get skill by profile id.
        /// </summary>
        /// <param name="id">Id of profile associated with the skills</param>
        IEnumerable<ProgrammerSkillDTO> GetSkillsOfProgrammer(string id);
        /// <summary>
        /// Get skill which the programmer doesn't have.
        /// </summary>
        /// <param name="id">Id of profile associated with the skills</param>
        IEnumerable<SkillDTO> GetSkillsThatTheProgrammerDoesNotHave(string id);
        /// <summary>
        /// Insert skill to profile.
        /// </summary>
        /// <param name="skill">Skill which will be added to programmer</param>
        void InsertSkillToProgrammer(ProgrammerSkillDTO skill);
        /// <summary>
        /// Updated skill of profile.
        /// </summary>
        /// <param name="skillId">Id of skill which match with id updated Skill
        /// <param name="skill">Skill which will be updated</param>
        void UpdateSkillOfProgrammer(int skillId, ProgrammerSkillDTO skill);
        /// <summary>
        /// Delete skill from profile.
        /// </summary>
        /// <param name="idProgrammer">Id of programmer profile which have removable skill</param>  
        /// <param name="skillId">Id of skill which will be deleted</param>  
        void DeleteSkillOfProgrammer(string idProgrammer, int skillId);
        /// <summary>
        /// Get all skills.
        /// </summary>
        IEnumerable<SkillDTO> GetSkills();
        /// <summary>
        /// Insert skill.
        /// </summary>
        /// <param name="skill">Skill which will be added</param>
        void Insert(SkillDTO skill);
        /// <summary>
        /// Delete skill.
        /// </summary>
        /// <param name="skillId">Id of skill which will be deleted</param>  
        void Delete(int skillId);
        /// <summary>
        /// Updated skill.
        /// </summary>
        /// <param name="skillId">Id of skill which match with id updated skill
        /// <param name="skill">Skill which will be updated</param>
        void Update(int skillId, SkillDTO skill);
    }
}
