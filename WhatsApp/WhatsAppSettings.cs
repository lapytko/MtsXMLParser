using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppApi;

namespace MtsXMLParser.WhatsApp
{
    public partial class WhatsAppSettings : Form
    {
        private WhatsAppProfile profile;
        private string password = "";
        private string name = "";
        public WhatsAppSettings()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.name = nameTextBox.Text.Trim();
            string phone = phoneTextBox.Text.Trim();
            this.profile = new WhatsAppProfile(name, phone);
            Register();
        }

        void Register()
        {
            try
            {
               this.password = WhatsAppApi.Register.WhatsRegisterV2.GetToken(profile.phone);
               codeTextBox.Text = this.password;
             // string id =  WhatsAppApi.Account.WhatsUser(profile.phone);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void WhatsAppSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                Program.settings.whatsAppToken = this.password;
                Program.settings.name = this.name;
            }
        }
    }
}
