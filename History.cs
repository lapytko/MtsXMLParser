using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MtsXMLParser.Types;

namespace MtsXMLParser
{
    class History
    {
        public Call call;
        public Sms sms;
        public string number;
        public DateTime date;
        public string duration;

        public History(Call input)
        {
            this.call = input;
            this.date = input.date;
            this.duration = input.duration;
        }
        public History(Sms input)
        {
            this.sms = input;
            this.date = input.date;
        }

        public static void AddCalls(List<History> input,ListBox destinition)
        {
            destinition.Items.Clear();
            foreach (History row in input)
            {
                destinition.Items.Add(row.ToString());
            }
        }

        public static void AddSms(List<History> input, ListBox dest)
        {
            dest.Items.Clear();
            foreach (History row in input)
            {
                dest.Items.Add(row.ToString());
            }
        }
        public static List<History> GenerateHistory(List<Call> input)
        {
            List<History> result = new List<History>();
            foreach (Call row in input)
            {
                History tmp = new History(row);
                result.Add(tmp);
            }
            return result;
        }

        public static void AddSms(List<Sms> list, ref List<History> destinition)
        {
            destinition = new List<History>();
            foreach (Sms row in list)
            {
                History tmp = new History(row);
                destinition.Add(tmp);
            }

        }

        public override string ToString()
        {
            if (call != null)
                return $"{this.date}    {this.duration}";
            if (sms != null)
                return $"{this.sms.number}    {this.date}";
            return "";
        }
    }
}

