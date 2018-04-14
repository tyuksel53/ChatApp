using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazlabII_Client.Models;
using System.Windows.Forms;

namespace YazlabII_Client
{
    public partial class KonusmaPenceresi : Form
    {
        public string targetUsername;
        public string targetIp;
        public KonusmaPenceresi(string param)
        {
            InitializeComponent();
            this.targetUsername = param.Split('&')[0];
            this.targetIp = param.Split('&')[1];
            MySocketClient.Instance.Handler += handler;
            lbHeader.Text = "Kullanıcı:" + MySocketClient.Instance.username + ", " + targetUsername + " ile konusuyor";
            pbHostPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            pbTargetPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            lbHostUsername.Text = MySocketClient.Instance.username;
            lbTargetUsername.Text = targetUsername;

            Bitmap dummy;
            if (AnaSayfa.resimler.TryGetValue(targetUsername, out dummy))
            {
                pbTargetPicture.Image = dummy;
            }

            if (AnaSayfa.resimler.TryGetValue(MySocketClient.Instance.username, out dummy))
            {
                pbHostPicture.Image = dummy;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lvMesajlar.Items.Add(MySocketClient.Instance.username + ": " + tbMessage.Text);
            MySocketClient.Instance.SendDataToServer("MessageToUser="+ MySocketClient.Instance.username + "&"+ targetIp+"&"+tbMessage.Text.Trim() + "&");
        }

        private void handler(object sender, MySocketEventHandler e)
        {
            if (e.NewClient == targetUsername)
            {
                lvMesajlar.Items.Add(string.Format("{0}: {1}"
                    , e.NewClient
                    , e.TextReceived));
            }
           
        }
    }
}
