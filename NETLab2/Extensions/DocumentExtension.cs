using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NET_Lab2.Extensions
{
    public static class DocumentExtension
    {
        public static XDocument ToXDocument(this XmlDocument xmlDoc)
        {
            return XDocument.Parse(xmlDoc.OuterXml);
        }
    }
}
