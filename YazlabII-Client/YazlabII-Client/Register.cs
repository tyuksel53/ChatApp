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

namespace YazlabII_Client
{
    public partial class Register : Form
    {
        private static readonly HttpClient client = new HttpClient();


        public Register()
        {
            InitializeComponent();
            tbNewUserPassword.PasswordChar = '*';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            KayitYap(tbNewUserUsername.Text, tbNewUserPassword.Text);
        }

        private async void KayitYap(string username,string password)
        {
            var serverUri = ConfigurationSettings.AppSettings["ServerUri"];
            var values = new Dictionary<string, string>
            {
                { "Username", username },
                { "Password", password }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(serverUri + "/Register/Register", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString == "\"Basarili\"")
            {
                MessageBox.Show("Kayit Gerçekleştirildi");
                this.Close();
            }
            else if (responseString == "\"Kullanıcı Adi Alinmis\"")
            {
                MessageBox.Show("Kullanıcı adi alinmis.Farklı bir kullanıcı adi ile tekrar deneyin");
            }
            else
            {
                MessageBox.Show("Bir şeyler ters gitti");
            }
            
        }
    }
}
