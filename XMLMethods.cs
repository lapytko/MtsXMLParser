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
                    if (td.Name == "td")
                    {
                        foreach (XmlNode tdc in td.ChildNodes)
                        {

                            if (tdc.Name == "c")
                            {
                                foreach (XmlNode c in tdc.ChildNodes)
                                {
                                    if ((c.Name == "s") &&(c.InnerText == "телефония"))
                                    {
                                        XmlNode node = tdc;
                                        result.Add(ComliteCall(node));
                                    }
                                }
                                    
                            }

                        }

                    }

                }
            }
            return result;
        }

        private int GetChildNumber(XmlNode node, string name)
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

        private Call ComliteCall(XmlNode x)
        {
             DateTime date = Convert.ToDateTime(x.ChildNodes[GetChildNumber(x, "d")].InnerText);
             string phone = x.ChildNodes[GetChildNumber(x,"n")].InnerText;
             string prov = x.ChildNodes[GetChildNumber(x, "zv")].InnerText;
             string duration = x.ChildNodes[GetChildNumber(x, "du")].InnerText;
             double cost = Convert.ToDouble(x.ChildNodes[GetChildNumber(x, "c")].InnerText) ;

             return  new Call(date,phone,prov,duration,cost);
        }

    }
}


