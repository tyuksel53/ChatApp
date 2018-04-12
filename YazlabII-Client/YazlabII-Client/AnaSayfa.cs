using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using YazlabII_Client.Models;

namespace YazlabII_Client
{
    public partial class AnaSayfa : Form
    {
        private HttpClient client = new HttpClient();
        private User LoggedInUser;
        private List<User> AllUsers = new List<User>();
        private Timer myTimer;
        private Timer myTimer2;
        public AnaSayfa(string currentUser)
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_Closing);
            
            LoggedInUser = new User(currentUser);
            lbUsername.Text = LoggedInUser.Username;
            lbOturumAcmaTarihi.Text = "Oturum Acma Zamanınız: \n"+ LoggedInUser.LastLoginTime;
            pbUserImg.Load("http://www.gravatar.com/avatar/6810d91caff032b202c50701dd3af745?d=identicon&r=PG");
            lvKullanicilar.Columns.Add("Username", "Username");
            lvKullanicilar.Columns.Add("Durum", "Durum");
            lvKullanicilar.View = View.Details;

            //lvKullanicilar.c
            myTimer = new Timer();
            myTimer.Tick += new EventHandler(timeer_fired);
            myTimer.Interval = 1000 /4 ; // 15 sn
            myTimer.Start();


            myTimer2 = new Timer();
            myTimer2.Tick += new EventHandler(timeer2_fired);
            myTimer2.Interval = 1000 / 4; // 15 sn
            myTimer2.Start();
        }


        private void timeer_fired(object sender, EventArgs e)
        {
            KullanicilariGetir();
        }

        private void timeer2_fired(object sender, EventArgs e)
        {
            IamHere(LoggedInUser.Username);
        }

        private async void IamHere(string username)
        {
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];

            var responseString = await client.GetStringAsync(serverUri + "/User/Iamhere?username="+username);
            
        }

        private async void KullanicilariGetir()
        {   
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];

            var responseString = await client.GetStringAsync(serverUri + "/User/AllUsers");
            AllUsers.Clear();
            JArray arry = JArray.Parse(responseString);
            lvKullanicilar.Items.Clear();
            foreach (var userObject in arry.Children<JObject>())
            {
                var user = new User();
                user.Id = Convert.ToInt32(userObject["Id"]);
                user.Username = userObject["Username"].ToString();
                user.Password = userObject["Password"].ToString();
                user.CurrentIp = userObject["Username"].ToString();
                user.ImgUrl = userObject["ImgUrl"].ToString();
                user.IsUserOnline = Convert.ToBoolean(userObject["IsUserOnline"]);
                user.LastLoginTime = userObject["LastLoginTime"].ToString();
                AllUsers.Add(user);

                if (user.Username != LoggedInUser.Username)
                {
                    if (user.IsUserOnline)
                    {
                        lvKullanicilar.Items.Add(new ListViewItem(new[] { user.Username, "ONLINE" })).SubItems[0].BackColor = Color.Green;
                    }
                    else
                    {
                        lvKullanicilar.Items.Add(new ListViewItem(new[] { user.Username, "OFFLINE" })).BackColor = Color.Red;
                    }
                }
                

            }

        }

        private void Form1_Closing(object sender, FormClosedEventArgs e)
        {
           Application.Exit();
        }

    }
}
