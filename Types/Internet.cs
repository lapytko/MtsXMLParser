using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MtsXMLParser.Types
{
    class Internet
    {
        public UInt64 bytes = 0;
        public UInt64 intern = 0;
        public UInt64 social = 0;
        public UInt64 messanger = 0;
        public UInt64 youtube = 0;

        public Internet()
        {
            this.bytes = GetByteCount();
            this.intern = GetByteCount("internet");
            this.social = GetByteCount("social");
            this.messanger = GetByteCount("messengers");
            this.youtube = GetByteCount("youtube");
        }

        private void Add(ref UInt64 counter, string count)
        {
            string x = count.Remove(count.Length - 1, 1);
            counter += Convert.ToUInt64(x);
        }

        private UInt64 GetByteCount(string type)
        {
            UInt64 counter = 0;
            foreach (XmlNode repo in MainForm.document.SelectNodes("report"))
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
                                    if ((c.Name == "n") && (c.InnerText.ToUpper().Contains(type.ToUpper())))
                                    {
                                        XmlNode node = tdc;
                                        Add(ref counter, GetFromNode(tdc));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return counter;
        }

        private UInt64 GetByteCount()
        {
            UInt64 counter = 0;
            foreach (XmlNode repo in MainForm.document.SelectNodes("report"))
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
                                    if ((c.Name == "s") && (c.InnerText == "данные"))
                                    {
                                        XmlNode node = tdc;
                                        Add(ref counter, GetFromNode(tdc));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return counter;
        }

        private string GetFromNode(XmlNode node)
        {
            string count = node.ChildNodes[XMLMethods.GetChildNumber(node, "du")].InnerText;
            return count;

        }

        public static void addItems(Internet input, TextBox dest)
        {
            string message = "";
            _ = input.bytes > 0
                ? message = $"За данный период было израсходовано {input.bytes.ToPrettySize(2)}"
                : message = "Нет интернет активности";
            bool stop = CompliteInternetDetails(message,dest);
            if (stop) return;
            _ = input.social > 0 ? message = $"Социальные сети: {input.social.ToPrettySize(2)}" : message = "";
            stop = CompliteInternetDetails(message,dest);
            _ = input.intern > 0 ? message = $"Браузер: {input.intern.ToPrettySize(2)}" : message = "";
            stop = CompliteInternetDetails(message,dest);
            _ = input.messanger > 0 ? message = $"Мессенджеры: {input.messanger.ToPrettySize(2)}" : message = "";
            stop = CompliteInternetDetails(message, dest);
            _ = input.youtube > 0 ? message = $"Youtube: {input.youtube.ToPrettySize(2)}" : message = "";
            stop = CompliteInternetDetails(message,dest);
        }

        public static bool CompliteInternetDetails(string info, TextBox txt)
        {
            if (info == "") return false;
            txt.Text += info + $"\r\n";
            if (info.Contains("Нет"))
            {
                return true;
            }

            return false;
        }
    }
}
