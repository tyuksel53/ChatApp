using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazlabII_Api.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string CurrentIp { get; set; }

        public string ImgUrl { get; set; }

        public bool IsUserOnline { get; set; }

        public string LastLoginTime { get; set; }
    }
}