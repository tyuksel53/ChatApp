using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            var response = KayitYap(tbNewUserUsername.Text, tbNewUserPassword.Text);

            

        }

        private async Task<string> KayitYap(string username,string password)
        {
            var values = new Dictionary<string, string>
            {
                { "Username", username },
                { "Password", password }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://192.168.0.28/yazlabi/Register/Register", content);

            var responseString = await response.Content.ReadAsStringAsync();

            MessageBox.Show(responseString);

            return responseString;
        }
    }
}
