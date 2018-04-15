using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace YazlabII_Api
{
    public class ChatHub : Hub
    {


        public void Hello()
        {
            Clients.All.hello("Hello World");
        }

        public void mesaj_gonder(string message, string gonderilecek_id)
        {

        }


        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;
            var username = Context.QueryString["username"];
            var ipAddress = GetIpAddress();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;



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