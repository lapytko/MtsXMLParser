using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtsXMLParser.Types
{

    class Call
    {
        public string date;
        public string number;
        public string provider;
        public string duration;
        public int cost;

        public Call(DateTime date, string number, string provider, string duration, int cost)
        {
            this.cost = cost;
            this.duration = duration;
            this.provider = provider;
            this.number = number;
        }
        
    }
}
