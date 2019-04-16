using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceToTheProject { get; set; }
        public string DescriptionOfTasks { get; set; }
        public string ProgrammerId { get; set; }
    }
}
