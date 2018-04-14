using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazlabII_Client
{
    public class MySocketEventHandler : EventArgs
    {
        public string NewClient { get; set; }
        public string TextReceived { get; set; }

        public MySocketEventHandler(string _newClient, string _textReceived)
        {
            NewClient = _newClient;
            TextReceived = _textReceived;
        }
    }
}
