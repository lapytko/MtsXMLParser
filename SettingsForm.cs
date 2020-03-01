using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MtsXMLParser.Viber;
using MtsXMLParser.WhatsApp;

namespace MtsXMLParser
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViberTokenForm askTokenForm = new ViberTokenForm();
            if (askTokenForm.ShowDialog() == DialogResult.OK)
            {
                Vlabel.Text = Program.settings.viberToken;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WhatsAppSettings whatsAppSettings = new WhatsAppSettings();
            if (whatsAppSettings.ShowDialog() == DialogResult.OK)
            {
                Wlabel.Text = Program.settings.whatsAppToken;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Vlabel.Text = Program.settings.viberToken;
            Wlabel.Text = Program.settings.whatsAppToken;
        }
    }
}
