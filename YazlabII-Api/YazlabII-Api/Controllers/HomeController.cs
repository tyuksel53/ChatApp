using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YazlabII_Api.Models.Managers;

namespace YazlabII_Api.Controllers
{
    public class HomeController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        [HttpGet]
        public HttpResponseMessage Text()
        {
            var toList = db.Users.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, "Hello World");
        }

        

    }
}
