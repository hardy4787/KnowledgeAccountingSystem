using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [MaxLength(64)]
        public int Age { get; set; }
        [Required]
        [MaxLength(64)]
        public string Email { get; set; }
        [MaxLength(64)]
        public string Address { get; set; }
        public string ImageProfileUrl { get; set; }
        [MaxLength(64)]
        public string Phone { get; set; }
        [MaxLength(64)]
        public string GitHub { get; set; }
    }
}