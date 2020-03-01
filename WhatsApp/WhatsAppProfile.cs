using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtsXMLParser.WhatsApp
{
    class WhatsAppProfile
    {
        public string name="";
        public string phone = "";

        public WhatsAppProfile(string name, string phone)
        {
            this.name = name;
            this.phone = phone;
        }
    }
}
