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
        private XmlDocument document;

        public XMLMethods(XmlDocument document)
        {
            this.document = document;
        }



        public List<Call> GetCalls()
        {
            var result = new List<Call>();
            foreach (XmlNode repo in document.SelectNodes("report"))
            {
                foreach (XmlElement td in repo.ChildNodes)
                {
                    foreach (XmlNode tdc in td.ChildNodes)
                    {
                        foreach (XmlNode c in tdc.ChildNodes)
                        {
                            if (c.Name == "c")
                            {

                            }
                            
                        }
                    }
                }

            }

            return result;
        }
    }
}


