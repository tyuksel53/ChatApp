using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YazlabII_Api.Models;
using YazlabII_Api.Models.Managers;

namespace YazlabII_Api.Controllers
{
    [Authorize]
    public class UserAuthenticationController : ApiController
    {
        DatabaseContext db = new DatabaseContext();

        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            var users = db.Users.ToList();
            List<User> allusers = new List<User>();
            foreach (var user in users)
            {
                if (ChatHub.OnlineUsers.Contains(user.Username))
                {
                    user.IsUserOnline = true;
                }
                else
                {
                    user.IsUserOnline = false;
                }

                allusers.Add(user);
            }
            return Request.CreateResponse(HttpStatusCode.OK, allusers);
        }
    }
}
