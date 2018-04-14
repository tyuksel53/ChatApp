﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Hatali Giris");
        }


        [HttpGet]
        public HttpResponseMessage AllUsers()
        {
            var allUsers = db.Users.ToList();
            List<User> updatedUsers = new List<User>();
            TcpClient dummy;
            foreach (var user in allUsers)
            {
                if (MySocket.Instance.ConnectedUsers.TryGetValue(user.CurrentIp, out dummy))
                {
                    user.IsUserOnline = true;
                }
                updatedUsers.Add(user);
            }

            return Request.CreateResponse(HttpStatusCode.OK,updatedUsers);
        }


    }
}
