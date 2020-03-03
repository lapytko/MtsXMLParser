using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MtsXMLParser.Types
{
    class Sms
    {
        public DateTime date;
        public string number;
        public int count;
        public double cost;
        public string provider;
        public bool type;

        public Sms( string date,  string number, int count, double cost, string provider, string type)
        {
            this.date = Convert.ToDateTime(date); ;
            this.cost = cost;
            this.count = count;
            this.provider = provider;
            this.type = makeType(type);
            string tmp_number = number = Normalaze(number);
            GetOperator(tmp_number);

        }

        public static List<Sms> GetSms()
        {
            var result = new List<Sms>();
            foreach (XmlNode repo in MainForm.document.SelectNodes("report"))
            {
                foreach (XmlElement td in repo.ChildNodes)
                {
                    if (td.Name == "td")
                    {
                        foreach (XmlNode tdc in td.ChildNodes)
                        {

                            if (tdc.Name == "c")
                            {
                                foreach (XmlNode c in tdc.ChildNodes)
                                {
                                    if ((c.Name == "s") && (c.InnerText.Contains("sms")))
                                    {
                                        XmlNode node = tdc;
                                        result.Add(Sms.ComliteSms(node));
                                    }
                                }

                            }

                        }

                    }

                }
            }
            return result;
        }
        
        public static Sms ComliteSms(XmlNode x)
        {
            string date = x.ChildNodes[XMLMethods.GetChildNumber(x, "d")].InnerText;
            string phone = x.ChildNodes[XMLMethods.GetChildNumber(x, "n")].InnerText;
            string prov = x.ChildNodes[XMLMethods.GetChildNumber(x, "zv")].InnerText;
            int count = Convert.ToInt32((x.ChildNodes[XMLMethods.GetChildNumber(x, "du")].InnerText));
            double cost = Convert.ToDouble(x.ChildNodes[XMLMethods.GetChildNumber(x, "c")].InnerText);
            string type = x.ChildNodes[XMLMethods.GetChildNumber(x, "s")].InnerText;

            return new Sms(date, phone, count, cost, prov, type);
        }
        public static List<Sms> FilterSms(List<Sms> input, bool type)
        {
            List<Sms> filtered = input.Where(x => x.type == type).ToList();
            return filtered;
        }
        private bool makeType(string value)
        {
            string[] splited = value.Split(' ');
            if (splited[1] == "i")
                return true;
            return false;
        }

        private void GetOperator(string phone)
        {
            if (phone.Contains(':'))
            {
                string[] splited = phone.Split(':');
                this.number = splited[1];
                if (splited[0].Equals("M"))
                    splited[0] += "TS";
                this.provider = splited[0];
            }
            else
            {
                this.number = phone;
            }
        }

        private string Normalaze(string phone)
        {
            if (this.type)
            {
                int pos = phone.IndexOf('-') + 2;
                return phone.Substring(pos);
            }

            return phone;
        }

        public string MakeTypeU()
        {
            if (this.type)
                return "Входящий";
            else
                return "Исходящий";
        }

        public static void SetDeDetails(Sms input, TextBox smsDetaisTextBox)
        {
            smsDetaisTextBox.Clear();
            smsDetaisTextBox.Text += ($"Номер телефона: {input.number}\r\n");
            smsDetaisTextBox.Text += ($"Тип: {input.MakeTypeU()}\r\n");
            smsDetaisTextBox.Text += ($"Оператор: {input.provider}\r\n");
            smsDetaisTextBox.Text += ($"Дата: {input.date}\r\n");
            smsDetaisTextBox.Text += ($"Количество: {input.count}\r\n");
            smsDetaisTextBox.Text += ($"Цена: {input.cost}\r\n");
            smsDetaisTextBox.Refresh();
        }

        public override string ToString()
        {
            return $"{this.number}  {this.date}";
        }
    }
}
