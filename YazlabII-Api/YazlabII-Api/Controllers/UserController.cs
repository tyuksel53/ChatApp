using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
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

        [HttpGet]
        public void sendAll()
        {
            MySocket.Instance.sendToAll("HEllo World");
        }

        [HttpPost]
        public async Task<object> Upload(string username)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
               
                try
                {
                    var file = await Request.Content.ReadAsByteArrayAsync();
                    var fileName = Request.Headers.GetValues("UploadedImage").FirstOrDefault();
                    var filePath = "/Uploads/";
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath(filePath) + fileName, file);
                    user.ImgUrl = "Uploads/" + fileName;
                    db.SaveChanges();
                    MySocket.Instance.sendToAll("!!!RESIM!!!");
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
           

            return null;
        }

        [HttpPost]
        public void UploadFile(string username)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), httpPostedFile.FileName);
                        user.ImgUrl = "Uploads/" + httpPostedFile.FileName;
                        // Save the uploaded file to "UploadedFiles" folder
                        httpPostedFile.SaveAs(fileSavePath);
                        db.SaveChanges();
                    }
                }
            }
           
        }


    }
}
