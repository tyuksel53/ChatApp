using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using YazlabII_Api.Models;
using YazlabII_Api.Models.Managers;

namespace YazlabII_Api
{
    public class ChatHub : Hub
    {
        DatabaseContext db = new DatabaseContext();
        public static List<string> OnlineUsers = new List<string>();
        public bool control = false;

        public void Hello()
        {
            Clients.All.hello("Hello World");
        }


        public void WantsToTalk(string message, string targetUsername,string fromUsername)
        {
            var connectionIds = db.Connections.Where(x => x.Username == targetUsername).Select(x => x.ConnectionID).ToList();
            Clients.Clients(connectionIds).wantsToTalk(message, fromUsername);
        }

        public void UserAcceptsTalk(string fromUser, string targetUser)
        {
            var connectionIds = db.Connections.Where(x => x.Username == targetUser).Select(x => x.ConnectionID).ToList();
            Clients.Clients(connectionIds).userAcceptsTalk("User Kabul Etti", fromUser);
        }

        public void mesaj_gonder(string message, string targetUser, string fromUser)
        {
            var connectionIds = db.Connections.Where(x => x.Username == targetUser).Select(x => x.ConnectionID).ToList();
            Clients.Clients(connectionIds).InComingMessage(message, fromUser);
        }

        public void UserDisconnect(string username)
        {
            var connectionIds = db.Connections.Where(x => x.Username == username).Select(x => x.ConnectionID).ToList();
            Clients.Clients(connectionIds).exit("çık");
            OnlineUsers.Remove(username);
            try
            {
                db.Connections.RemoveRange(db.Connections.Where(x => x.Username == username));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;
            var username = Context.QueryString["username"];
            var ipAddress = GetIpAddress();
            var user = db.Users.FirstOrDefault(x => x.Username == username);
            if (user != null)
            {
                user.CurrentIp = ipAddress;
                if (!OnlineUsers.Contains(user.Username))
                {
                    OnlineUsers.Add(user.Username);
                }

                var connection = new Connection()
                {
                    ConnectionID = connectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Username = username
                };

                db.Connections.Add(connection);
                db.SaveChanges();


            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            var connectionId = Context.ConnectionId;
            var toRemoveConnection = db.Connections.FirstOrDefault(x => x.ConnectionID == connectionId);
            if (toRemoveConnection != null)
            {
                try
                {
                    var username = toRemoveConnection.Username;
                    db.Connections.Remove(toRemoveConnection);
                    db.SaveChanges();

                    var hasUserDisConnect = db.Connections.Where(x => x.Username == username).ToList();
                    if (!(hasUserDisConnect.Count > 0 ) )
                    {
                        OnlineUsers.Remove(username);
                    }
                }
                catch (Exception ex)
                {
                   Debug.WriteLine(ex.ToString());
                }

            }

            return base.OnDisconnected(stopCalled);
        }

        protected string GetIpAddress()
        {
            string ipAddress;
            object tempObject;

            Context.Request.Environment.TryGetValue("server.RemoteIpAddress", out tempObject);

            if (tempObject != null)
            {
                ipAddress = (string)tempObject;
            }
            else
            {
                ipAddress = "";
            }

            return ipAddress;
        }
    }
}