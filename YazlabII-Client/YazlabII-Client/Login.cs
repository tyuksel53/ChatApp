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
using YazlabII_Client.Models;

namespace YazlabII_Client
{
    public partial class Login : Form
    {
        private static readonly HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_Closing);
            tbPassword.PasswordChar = '*';
        }

        private void Form1_Closing(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Register pencere = new Register();
            pencere.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisYap(tbUsername.Text,tbPassword.Text);
        }



        private async void GirisYap(string username, string password)
        {
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];

            var response = await client.PostAsync(serverUri + "/User/Login?username="+username+"&password="+password,null);

            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString == "\"Hatali Giris\"")
            {
                MessageBox.Show("Yanlis kullanici Adi veya Şifre");
            }
            else
            {
                var logedInUser = new User(responseString);
                MessageBox.Show("Giris basarili");
                AnaSayfa pencere = new AnaSayfa(responseString);
                pencere.Show();
                this.Hide();
                try
                {
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Bir şeyler ters gitti");
                }
            }


        }
    }
}
