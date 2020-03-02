using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        string GetNumberFromString(string phone)
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
