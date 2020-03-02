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
        private List<History> historyCalls;

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
                addItems();
                listBox1.SelectedIndex = -1;
            }
        }

        void addItems()
        {
            var removed = RemoveDublicate(this.calls);
            foreach (string row in removed )
            {
                listBox1.Items.Add(row);
            }
            
        }

        private List<string> RemoveDublicate(List<Call> input)
        {
            var removed = input.Select(x => x.number).Distinct().ToList();
            return removed;
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

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            
            tableLayoutPanel1.Width = this.Width;
            tableLayoutPanel1.Height = this.Height;
        }

        private List<Call> FilterByNumber(List<Call> input, string number)
        {
            List<Call> filtered = input.Where(x => x.number == number).ToList();
            return filtered;
        }

        private List<History> GenerateHistory(List<Call> input)
        {
            List<History> result = new List<History>();
            foreach (Call row in input)
            {
             History tmp = new History(row);
             result.Add(tmp);
            }

            return result;
        }

        void AddToHistory(List<History> input)
        {
            historyListBox.Items.Clear();
            foreach (History row in input)
            {
                historyListBox.Items.Add(row.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = listBox1.SelectedIndex;
            string value = listBox1.Items[selected].ToString();
            List<Call> HistoryByNumber = FilterByNumber(this.calls, value);
             historyCalls= GenerateHistory(HistoryByNumber);
            AddToHistory(historyCalls);
        }


        private void SetDeDetails(Call input)
        {
            DetailstextBox.Clear();
            DetailstextBox.Text+=($"Номер телефона: {input.number}\r\n");
            DetailstextBox.Text += ($"Дата: {input.date}\r\n");
            DetailstextBox.Text += ($"Прожолжительность: {input.duration}\r\n");
            DetailstextBox.Refresh();
        }

        private void historyListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = historyListBox.SelectedIndex;
            Call call = historyCalls[selected].call;
            SetDeDetails(call);
        }
    }
}
