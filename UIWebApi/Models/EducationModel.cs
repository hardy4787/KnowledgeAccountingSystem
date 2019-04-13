using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class EducationModel
    {
        public int? Id { get; set; }
        public string Level { get; set; }
        [Required]
        [MaxLength(64)]
        public string NameInstitution { get; set; }
        [Required]
        public DateTime? EntryDate { get; set; }
        public DateTime? CloseDate { get; set; }
        [Required]
        public string ProgrammerId { get; set; }
    }
}