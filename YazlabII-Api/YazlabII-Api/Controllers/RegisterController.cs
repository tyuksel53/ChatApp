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
    public class RegisterController : ApiController
    {
        DatabaseContext db = new DatabaseContext();

        [HttpPost]
        public HttpResponseMessage Register(User newUser)
        {
             var check = db.Users.FirstOrDefault(x => x.Username == newUser.Username);
            if (check == null)
            {
                var ip = HttpContext.Current.Request.UserHostAddress;
                newUser.CurrentIp = ip;
                newUser.LastLoginTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                db.Users.Add(newUser);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Basarili");
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, "Kullanıcı Adi Alinmis");
        }
    }

}
