using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
        public static Dictionary<string,Bitmap> resimler = new Dictionary<string, Bitmap>();
        public bool control = false;
        

        public AnaSayfa(string currentUser)
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_Closing);
            LoggedInUser = new User(currentUser);
            MySocketClient.Instance.SetUsername(LoggedInUser.Username);
            MySocketClient.Instance.ConnectToServer();
            
            MySocketClient.Instance.ReadDataAsync(MySocketClient.Instance.mClient);
            MySocketClient.Instance.SendDataToServer("Login="+LoggedInUser.Username + "&" + LoggedInUser.Password + "&");
            lbUsername.Text = LoggedInUser.Username;
            lbOturumAcmaTarihi.Text = "Oturum Acma Zamanınız: \n"+ LoggedInUser.LastLoginTime;
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];
            if (LoggedInUser.ImgUrl != "")
            {
                pbUserImg.SizeMode = PictureBoxSizeMode.StretchImage;
                pbUserImg.Load(serverUri + LoggedInUser.ImgUrl);
            }
            lvKullanicilar.Columns.Add("Username                   ");
            lvKullanicilar.Columns.Add("Durum");
            lvKullanicilar.Columns.Add("IP");
            
            lvKullanicilar.View = View.Details;
            lvKullanicilar.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //lvKullanicilar.c
            myTimer = new Timer();
            myTimer.Tick += new EventHandler(timeer_fired);
            myTimer.Interval = 1000 /4 ; // 15 sn
            myTimer.Start();
        }


        private void timeer_fired(object sender, EventArgs e)
        {
            KullanicilariGetir();
        }

        private async void KullanicilariGetir()
        {
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];

            var responseString = await client.GetStringAsync(serverUri + "/User/AllUsers");
            AllUsers.Clear();
            JArray arry = JArray.Parse(responseString);
            lvKullanicilar.Items.Clear();
            ImageList il = new ImageList();
            il.ImageSize = new Size(50,50);
            int i = 0;
            foreach (var userObject in arry.Children<JObject>())
            {
                var user = new User();
                user.Id = Convert.ToInt32(userObject["Id"]);
                user.Username = userObject["Username"].ToString();
                user.Password = userObject["Password"].ToString();
                user.CurrentIp = userObject["CurrentIp"].ToString();
                user.ImgUrl = userObject["ImgUrl"].ToString();
                user.IsUserOnline = Convert.ToBoolean(userObject["IsUserOnline"]);
                user.LastLoginTime = userObject["LastLoginTime"].ToString();
                if (user.ImgUrl != "")
                {
                    il.Images.Add(user.Username,LoadImage(serverUri + user.ImgUrl,user.Username));
                }

                if (user.Username != LoggedInUser.Username)
                {
                    AllUsers.Add(user);
                    if (user.IsUserOnline)
                    {
                        var mundi = new ListViewItem(new[] {user.Username, "ONLINE", user.CurrentIp});
                        mundi.ImageKey = user.Username;
                        lvKullanicilar.Items.Add(mundi).SubItems[0].BackColor = Color.Green;
                    }
                    else
                    {
                        var mundi = new ListViewItem(new[] { user.Username, "OFFLINE", user.CurrentIp });
                        mundi.ImageKey = user.Username;
                        lvKullanicilar.Items.Add(mundi).BackColor = Color.Red;
                    }
                }


            }
            lvKullanicilar.SmallImageList = il;

        }

        private Image LoadImage(string url,string username)
        {
            Bitmap dummy;
            if(resimler.TryGetValue(username,out dummy))
            {
                return dummy;
            }
            
            System.Net.WebRequest request =
                System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream =
                response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);

            responseStream.Dispose();
            resimler.Add(username,bmp);
            return bmp;
        }

        private void Form1_Closing(object sender, FormClosedEventArgs e)
        {
            if (!control)
            {
                MySocketClient.Instance.Disconnect();
                Application.Exit();
            }
        }

        private void lvKullanicilar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string isUserOnline = lvKullanicilar.SelectedItems[0].SubItems[1].Text;
            if (isUserOnline == "ONLINE")
            {
                string ip = lvKullanicilar.SelectedItems[0].SubItems[2].Text;
                MySocketClient.Instance.SendDataToServer("UserWantsToTalkTo=" + LoggedInUser.Username + "&"
                                                         + ip + "&");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MySocketClient.Instance.Disconnect();
            control = true;
            Login pencere = new Login();
            pencere.Show();

            List<Form> openForms = new List<Form>();

            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name != "Login")
                    f.Close();
            }

        }

        private void btnFileUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName; // get name of file
                pbUserImg.Load(path);
                Upload(path);
            }
        }

        private void Upload(string fileName)
        {
            var client = new WebClient();
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];
            var uri = new Uri(serverUri+"/User/Upload?username="+LoggedInUser.Username );
            try
            {
                client.Headers.Add("fileName", System.IO.Path.GetFileName(fileName));
                var data = System.IO.File.ReadAllBytes(fileName);
                client.UploadDataAsync(uri, data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
