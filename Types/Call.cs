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
        public bool type=false;

        public Call(DateTime date, string number, string provider, string duration, double cost)
        {
            this.date = date;
            this.cost = cost;
            this.duration = duration;
            this.provider = provider;
            if (number != null)
            {
                this.type = SetCallType(number);
                CorrectNumber();
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
                    this.number =splited[1];
                    if (splited[0].Contains("<--"))
                    { return true; }
                }
                else
                {
                    this.number = splited[0];
                }
            }
            return false;
        }

        private void CorrectNumber()
        {
            if (this.number != null)
            {
                if (this.number[0].Equals("+"))
                { return; }
                else
                {
                    string corrected = "+";
                    corrected += number;
                    this.number = corrected;
                }
            }
            else
            {
                this.number = "Номер не определён";
            }

        }

        
    }
}
