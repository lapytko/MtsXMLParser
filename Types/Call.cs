using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MtsXMLParser.Types
{
    public class Call
    {
        public DateTime date;
        public string number;
        public string provider;
        public string duration;
        public double cost;
        public bool type = false;

        public Call() { }
        public Call(DateTime date, string number, string provider, string duration, double cost)
        {
            this.date = date;
            this.cost = cost;
            this.duration = duration;
            this.provider = provider;
            if (number != null)
            {
                this.type = SetCallType(number);
                CorrectNumber(number);
            }
            else
            {
                this.number = "Номер не определён";
            }

        }

        private static List<string> RemoveDublicate(List<Call> input)
        {
            var removed = input.Select(x => x.number).Distinct().ToList();
            return removed;
        }

        public static void SetDeDetails(Call input, TextBox txt)
        {
            txt.Clear();
            txt.Text += ($"Номер телефона: {input.number}\r\n");
            txt.Text += ($"Тип: {input.MakeTypeU()}\r\n");
            txt.Text += ($"Оператор: {input.provider}\r\n");
            txt.Text += ($"Дата: {input.date}\r\n");
            txt.Text += ($"Прожолжительность: {input.duration}\r\n");
            txt.Text += ($"Цена: {input.cost}\r\n");
            txt.Refresh();
        }

        public static void addItems(ListBox destinition, List<Call> input)
        {
            var removed = RemoveDublicate(input);
            foreach (string row in removed)
            {
                destinition.Items.Add(row);
            }

        }
        public  static Call ComliteCall(XmlNode x)
        {
            DateTime date = Convert.ToDateTime(x.ChildNodes[XMLMethods.GetChildNumber(x, "d")].InnerText);
            string phone = x.ChildNodes[XMLMethods.GetChildNumber(x, "n")].InnerText;
            string prov = x.ChildNodes[XMLMethods.GetChildNumber(x, "zv")].InnerText;
            string duration = x.ChildNodes[XMLMethods.GetChildNumber(x, "du")].InnerText;
            double cost = Convert.ToDouble(x.ChildNodes[XMLMethods.GetChildNumber(x, "c")].InnerText);

            return new Call(date, phone, prov, duration, cost);
        }

        public static List<Call> GetCalls()
        {
            var result = new List<Call>();
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
                                    if ((c.Name == "s") && (c.InnerText == "телефония"))
                                    {
                                        XmlNode node = tdc;
                                        result.Add(ComliteCall(node));
                                    }
                                }

                            }

                        }

                    }

                }
            }
            return result;
        }

        public static List<Call> FilterByNumber(List<Call> input, string number)
        {
            List<Call> filtered = input.Where(x => x.number == number).ToList();
            return filtered;
        }

        private bool SetCallType(string phone)
        {
            if (phone.Contains(":"))
            {
                string[] splited = phone.Split(':');
                if (splited.Length > 1)
                {
                    this.number = splited[1];
                    if (splited[0].Contains("<--"))
                    { return true; }
                }
                else
                {
                    this.number = splited[0];
                }
            }
            else
            {
                if (phone.Contains("<--"))
                {
                    int tmp = phone.IndexOf('-') + 1;
                    string x = phone.Substring(tmp + 1);
                    this.number = x;
                    return true;
                }
            }
            return false;
        }

        private string GetNumberFromString(string phone)
        {
            if (phone.Contains(":"))
            {
                string[] splited = phone.Split(':');
                if (splited.Length > 1)
                {
                    return splited[1];
                }
                else
                {
                    return splited[0];
                }
            }
            else
            {
                return phone;
            }
        }

        private void CorrectNumber(string number)
        {
            if (number != null)
            {
                string splitedN = GetNumberFromString(number);
                if (splitedN[0] == '+')
                {
                    this.number = splitedN;
                }
                else
                {
                    if (splitedN.Contains("<--"))
                    {
                        int tmp = splitedN.IndexOf('-') + 1;
                        string x = splitedN.Substring(tmp + 1);
                        this.number = x;
                        return;
                    }
                    else
                    {
                        string corrected = "+";
                        corrected += splitedN;
                        this.number = corrected;
                    }
                }
            }
        }

        /// <summary>
        /// Преобразует тип звонка в понятный для человека
        /// </summary>
        public string MakeTypeU()
        {
            if (this.type)
                return "Входящий";
            else
                return "Исходящий";
        }

    }
}
