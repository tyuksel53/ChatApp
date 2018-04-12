using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
                user.LastLoginTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                user.IsUserOnline = true;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Hatali Giris");
        }


    }
}
