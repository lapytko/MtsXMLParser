using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MtsXMLParser.Types;

namespace MtsXMLParser
{
    class History
    {
        public Call call;
        public string number;
        public DateTime date;
        public string duration;

        public History(Call input)
        {
            this.call = input;
            this.date = input.date;
            this.duration = input.duration;

        }

        public override string ToString()
        {
            return $"{this.date}    {this.duration}";
        }
    }
}

