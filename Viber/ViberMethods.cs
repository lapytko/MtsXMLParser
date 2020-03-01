using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Viber.Bot;

namespace MtsXMLParser.Viber
{
    class ViberMethods
    {
        private ViberBotClient appViber;

        public ViberMethods()
        {
            appViber = new ViberBotClient(Program.settings.viberToken);
            var  user = appViber.GetUserDetailsAsync("wwqyeui==");
        }
    }
}
