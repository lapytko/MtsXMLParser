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

        private Call ComliteCall(XmlNode x)
        {
             DateTime date = Convert.ToDateTime(x.ChildNodes[0].InnerText);
             string phone = x.ChildNodes[1].InnerText;
             string prov = x.ChildNodes[3].InnerText;
             string duration = x.ChildNodes[6].InnerText;
             double cost = Convert.ToDouble(x.ChildNodes[7].InnerText) ;

             return  new Call(date,phone,prov,duration,cost);
        }
    }
}


