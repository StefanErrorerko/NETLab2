using System.Xml;
using System.Xml.Linq;
using NET_Lab2.Extensions;

namespace NET_Lab2.XmlProcessors
{
    public class ReaderXml
    {
        public readonly XDocument XmlAuthors;
        public readonly XDocument XmlArticles;
        public readonly XDocument XmlMags;
        public readonly XDocument XmlDocs;

        public ReaderXml()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load("authors.xml");
            XmlAuthors = xmlDoc.ToXDocument();
            
            xmlDoc.Load("magazines.xml");
            XmlMags = xmlDoc.ToXDocument();

            xmlDoc.Load("articles.xml");
            XmlArticles = xmlDoc.ToXDocument();

            xmlDoc.Load("editordocuments.xml");
            XmlDocs = xmlDoc.ToXDocument();
        }
    }
}
