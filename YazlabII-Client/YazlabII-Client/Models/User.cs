using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace YazlabII_Client.Models
{
    class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string CurrentIp { get; set; }

        public string ImgUrl { get; set; }

        public bool IsUserOnline { get; set; }

        public string LastLoginTime { get; set; }

        public User(string json)
        {
            JObject userObject = JObject.Parse(json);
            this.Id = Convert.ToInt32(userObject["Id"]);
            this.Username = userObject["Username"].ToString();
            this.Password = userObject["Password"].ToString();
            this.CurrentIp = userObject["Username"].ToString();
            this.ImgUrl = userObject["ImgUrl"].ToString();
            this.IsUserOnline = Convert.ToBoolean(userObject["IsUserOnline"]);
            this.LastLoginTime = userObject["LastLoginTime"].ToString();
        }

        public User()
        {

        }
    }
}
