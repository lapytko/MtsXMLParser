using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Com.CloudRail.SI.ServiceCode.Commands.CodeRedirect;
using Com.CloudRail.SI.Types;

namespace MtsXMLParser.Viber
{
    public partial class ViberTokenForm : Form
    {
        private string tokken="";
        
        public ViberTokenForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.tokken = tokenBox.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ViberTokenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                Program.settings.viberToken = this.tokken;
                ViberMethods methods = new ViberMethods();
            }
        }
    }
}
