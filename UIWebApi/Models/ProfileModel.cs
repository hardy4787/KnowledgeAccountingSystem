using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIWebApi.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImageProfileUrl { get; set; }
        public string Phone { get; set; }
        public string GitHub { get; set; }
    }
}