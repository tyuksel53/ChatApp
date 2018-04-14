using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using YazlabII_Api.Models;
using YazlabII_Api.Models.Managers;

namespace YazlabII_Api.Controllers
{
    public class UserController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [HttpPost]
        public HttpResponseMessage Login([FromUri] string username, [FromUri] string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                user.CurrentIp = HttpContext.Current.Request.UserHostAddress;
                user.LastLoginTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                user.IsUserOnline = true;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Hatali Giris");
        }


        [HttpGet]
        public HttpResponseMessage AllUsers()
        {

            var beniDovdulerabi = MySocket.Instance.mClients;
            var allUsers = db.Users.ToList();
            List<User> updatedUsers = new List<User>();
            foreach (var user in allUsers)
            {
                var span = DateTime.Now - DateTime.Parse(user.LastLoginTime);
                if (span.Days == 0 && span.Hours == 0 && span.Minutes == 0 && span.Seconds <= 15)
                {
                    user.IsUserOnline = true;
                }
                else
                {
                    user.IsUserOnline = false;
                }

                updatedUsers.Add(user);
            }

            return Request.CreateResponse(HttpStatusCode.OK,updatedUsers);
        }


    }
}
