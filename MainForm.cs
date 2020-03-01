using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MtsXMLParser.Types;

namespace MtsXMLParser
{
    public partial class MainForm : Form
    {
        private XmlDocument document;
        List<Call> calls = new List<Call>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                document= new XmlDocument();
                document.Load(openFileDialog.FileName);
                XMLMethods methods = new XMLMethods(document);
                calls = methods.GetCalls();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Вы уверены, что хотите выйти ?";
            if (MessageBox.Show(message, "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
               Application.Exit();
            }
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            if(settingsForm.ShowDialog(this) == DialogResult.OK)
            { }
        }
    }
}
