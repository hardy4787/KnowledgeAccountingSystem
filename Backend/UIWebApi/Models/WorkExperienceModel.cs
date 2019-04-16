using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class WorkExperienceModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Company { get; set; }
        [Required]
        [MaxLength(64)]
        public string Position { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        public DateTime? CloseDate { get; set; }
        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
        [Required]
        public string ProgrammerId { get; set; }
    }
}