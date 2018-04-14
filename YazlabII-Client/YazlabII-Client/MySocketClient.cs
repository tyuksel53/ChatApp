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

        private IPAddress mServerAddress;
        private int mServerPortNumber;
        public TcpClient mClient;

        public static MySocketClient Instance
        {
            get { return instance; }
        }

        public MySocketClient()
        {
            IPAddress ipAddress = null;
            mClient = null;
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
                        DialogResult dialogResult = MessageBox.Show(message, "Konusma Daveti", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //do something
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                        }
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
