using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MtsXMLParser.Types;

namespace MtsXMLParser
{
    class XMLMethods
    {
        public static int GetChildNumber(XmlNode node, string name)
        {
            int i = 0;
            foreach (XmlNode row in node.ChildNodes)
            {
                if (row.Name == name)
                    return i;
                i++;
            }
            return i;
        }
    }
}


