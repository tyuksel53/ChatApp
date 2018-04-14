using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazlabII_Client
{
    public sealed class MySocketClient
    {
        private static readonly MySocketClient instance = new MySocketClient();
        public EventHandler<MySocketEventHandler> Handler;
        private IPAddress mServerAddress;
        private int mServerPortNumber;
        public TcpClient mClient;
        public string username;
        public List<string> currentChats;
        public static MySocketClient Instance
        {
            get { return instance; }
        }

        public void SetUsername(string _username)
        {
            username = _username;
        }

        protected void OnRaise(MySocketEventHandler e)
        {
            EventHandler<MySocketEventHandler> handler = Handler;

            if (mClient != null)
            {
                handler(this, e);
            }
        }


        public MySocketClient()
        {
            IPAddress ipAddress = null;
            mClient = null;
            currentChats = new List<string>();
            if (IPAddress.TryParse(ConfigurationSettings.AppSettings["ServerIp"], out ipAddress))
            {
                mServerAddress = ipAddress;
            }
            mServerPortNumber = Convert.ToInt32(ConfigurationSettings.AppSettings["ServerPortNumber"]);
        }


        public async Task ConnectToServer()
        {
            if (mClient == null)
            {
                mClient = new TcpClient();
            }

            try
            {
                mClient.ConnectAsync(mServerAddress, mServerPortNumber);
                Debug.WriteLine("Sunucuya bağlandık");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task ReadDataAsync(TcpClient client)
        {
            try
            {
                StreamReader reader = new StreamReader(client.GetStream());
                char[] buff = new char[1000];
                int readByteCount = 0;

                while (true)
                {
                    readByteCount = await reader.ReadAsync(buff, 0, buff.Length);
                    if (readByteCount <= 0)
                    {
                        Debug.WriteLine("Sunucu ile baglanti kesildi");
                        client.Close();
                        break;
                    }

                    string message = new string(buff);

                    if (message.StartsWith("UserWantsToTalkTo="))
                    {
                        message = message.Replace("UserWantsToTalkTo=", "");
                        if (!currentChats.Contains(message.Split(' ')[0]))
                        {
                            var param = message.Split('&');
                            DialogResult dialogResult = MessageBox.Show(param[0], "Konusma Daveti", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                currentChats.Add(message.Split(' ')[0]);
                                SendDataToServer("UserKonusmayıKabulEtti=" + param[1] + "&" + username + "&");
                                KonusmaPenceresi pencere = new KonusmaPenceresi(message.Split(' ')[0] + "&" + param[1]);
                                pencere.Show();
                            }
                        }
                       
                    }

                    if (message.StartsWith("StartConversition="))
                    {
                        message = message.Replace("StartConversition=", "");
                        KonusmaPenceresi pencere = new KonusmaPenceresi(message);
                        pencere.Show();
                    }

                    if (message.StartsWith("MessageReceived="))
                    {
                        message = message.Replace("MessageReceived=", "");
                        var parameters = message.Split('&');
                        OnRaise(new MySocketEventHandler(parameters[0], parameters[1]));
                    }

                    Debug.WriteLine("Message Received: " + message );

                    Array.Clear(buff,0,buff.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async Task SendDataToServer(string userInput)
        {
            if (String.IsNullOrWhiteSpace(userInput))
            {
                return;
            }

            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    StreamWriter writer = new StreamWriter(mClient.GetStream());
                    writer.AutoFlush = true;

                    await writer.WriteAsync(userInput);
                    Debug.WriteLine("Data send");
                }
            }
        }

        public void Disconnect()
        {
            if (mClient != null)
            {
                if (mClient.Connected)
                {
                    mClient.Close();
                }
            }
        }

    }
}
