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
        public static XmlDocument document;
        List<Call> calls = new List<Call>();
        List<Sms> smsList = new List<Sms>();
        private List<History> historyCalls;
        private List<History> historySms;
        private Internet internet;

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
                this.internet = new Internet();
                calls = Call.GetCalls();
                smsList = Sms.GetSms();
                Call.addItems(listBox1,calls);
                Internet.addItems(this.internet,InternettextBox);
                listBox1.SelectedIndex = -1;
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
         
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            tabPage1.Width = this.Width;
            tableLayoutPanel1.Width = tabPage1.Width;
            tableLayoutPanel1.Height = tabPage1.Height;
            
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = listBox1.SelectedIndex;
            string value = listBox1.Items[selected].ToString();
            List<Call> HistoryByNumber = Call.FilterByNumber(this.calls, value);
             historyCalls= History.GenerateHistory(HistoryByNumber);
            History.AddCalls(historyCalls,historyListBox);
        }
        
        private void historyListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = historyListBox.SelectedIndex;
            Call call = historyCalls[selected].call;
            Call.SetDeDetails(call,DetailstextBox);
        }

        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            DetailstextBox.Height = tableLayoutPanel1.Height - 20;
        }
        
        private void typeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Sms> filtered;
            int selected = typeListBox.SelectedIndex;
            string txt = typeListBox.Items[selected].ToString();
            if (txt == "Входящие")
            {
                filtered = Sms.FilterSms(this.smsList, true);
            }
            else
            {
                filtered = Sms.FilterSms(this.smsList, false);
            }
            History.AddSms(filtered, ref this.historySms);
            History.AddSms(this.historySms, smsHistoryList);
        }


        private void smsHistoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = smsHistoryList.SelectedIndex;
            Sms txt = this.historySms[selected].sms;
            Sms.SetDeDetails(txt, smsDetaisTextBox);
        }
    }
}
