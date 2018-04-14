﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using YazlabII_Api.Models;
using YazlabII_Api.Models.Managers;

namespace YazlabII_Api
{
    public sealed class MySocket
    {
        DatabaseContext db = new DatabaseContext();

        private static readonly MySocket instance = new MySocket();

        private IPAddress ip;
        private int PortNum;
        private TcpListener Listener;
        public bool KeepRuning { get; set; }
        public List<TcpClient> mClients;
        public Dictionary<string, User> ConnectedUsers;

        public static MySocket Instance
        {
            get { return instance; }
        }

        public MySocket()
        {
            mClients = new System.Collections.Generic.List<TcpClient>();
            ConnectedUsers = new Dictionary<string, User>();
        }


        public async void StartConnection(IPAddress ip = null,int port = 23000)
        {
            if (ip == null)
            {
                ip = IPAddress.Any;
            }

            Listener = new TcpListener(ip,port);

            try
            {
                Listener.Start();
                KeepRuning = true;
                while (KeepRuning)
                {
                    var returnByAccept = await Listener.AcceptTcpClientAsync();
                    mClients.Add(returnByAccept);
                    Debug.WriteLine("İstemci baglandi");
                    TakeCareOfTcpClient(returnByAccept);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async void TakeCareOfTcpClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[1000];
                while (KeepRuning)
                {
                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    if (nRet == 0)
                    {
                        User dummy;
                        if (ConnectedUsers.TryGetValue(paramClient.Client.RemoteEndPoint.ToString(), out dummy))
                        {
                            ConnectedUsers.Remove(paramClient.Client.RemoteEndPoint.ToString());
                            dummy = db.Users.FirstOrDefault(x => x.Username == dummy.Username);
                            if (dummy != null)
                            {
                                dummy.IsUserOnline = false;
                                db.SaveChanges();
                            }
                        }
                        RemoveClient(paramClient);
                        Debug.WriteLine("İstemci ayrildi");
                        break;
                    }

                    string receivedData = new string(buff);
                    if (receivedData.StartsWith("Login="))
                    {
                        receivedData =  receivedData.Replace("Login=", "");
                        var usernameAndPass = receivedData.Split('&');
                        var username = usernameAndPass[0];
                        var password = usernameAndPass[1];

                        var user = db.Users.FirstOrDefault(x =>
                            x.Username == username && x.Password == password);

                        if (user != null)
                        {
                            var userIp = paramClient.Client.RemoteEndPoint.ToString();
                            user.CurrentIp = userIp;
                            ConnectedUsers.Add(userIp,user);
                            db.SaveChanges();
                        }

                    }
                    Debug.WriteLine("Data Received: " + receivedData);
                    Array.Clear(buff,0,buff.Length);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                RemoveClient(paramClient);
            }
        }

        public async void sendToAll(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return;
            }

            try
            {
                byte[] buff = Encoding.ASCII.GetBytes(message);
                foreach (var tcpClient in mClients)
                {
                    await tcpClient.GetStream().WriteAsync(buff, 0, buff.Length);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public void StopServer()
        {
            try
            {
                if (Listener != null)
                {
                    Listener.Stop();
                }

                foreach (var tcpClient in mClients)
                {
                    tcpClient.Close();
                }

                mClients.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
            }
        }
    }
}