using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtsXMLParser.Types
{

    class Call
    {
        public DateTime date;
        public string number;
        public string provider;
        public string duration;
        public double cost;

        public Call(DateTime date, string number, string provider, string duration, double cost)
        {
            this.date = date;
            this.cost = cost;
            this.duration = duration;
            this.provider = provider;
            this.number = number;
        }
        
    }
}
