using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class SkillModel
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string Description { get; set; }
    }
}