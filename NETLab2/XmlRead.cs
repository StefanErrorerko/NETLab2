using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using NET_Lab2.Entity;
using NET_Lab2.Instruments;

namespace NET_Lab2
{
    internal class XmlRead
    {
        internal readonly XDocument Xmlauthors;
        internal readonly XDocument Xmlarticles;
        internal readonly XDocument Xmlmags;
        internal readonly XDocument Xmldocs;

        internal XmlRead()
        {
            Xmlauthors = XDocument.Load("authors.xml");
            foreach (XElement userElement in Xmlauthors.Element("authors").Elements("author"))
            {
                XElement authorid = userElement.Element("authorid");
                XElement surname = userElement.Element("surname");
                XElement name = userElement.Element("name");
                XElement secondname = userElement.Element("secondname");
                XElement organisation = userElement.Element("organisation");
            }

            Xmlmags = XDocument.Load("magazines.xml");
            foreach (XElement userElement in Xmlmags.Element("magazines").Elements("magazine"))
            {
                XElement magid = userElement.Element("magid");
                XElement name = userElement.Element("name");
                XElement established = userElement.Element("established");
                XElement circulation = userElement.Element("circulation");
                XElement frequency = userElement.Element("frequency");
            }

            Xmlarticles = XDocument.Load("articles.xml");
            foreach (XElement userElement in Xmlarticles.Element("articles").Elements("article"))
            {
                XElement articleid = userElement.Element("articleid");
                XElement name = userElement.Element("name");
                XElement author = userElement.Element("authorid");
            }

            Xmldocs = XDocument.Load("editordocuments.xml");
            foreach (XElement userElement in Xmldocs.Element("docs").Elements("doc"))
            {
                XElement docid = userElement.Element("docid");
                XElement date = userElement.Element("date");
                XElement articleid = userElement.Element("articleid");
                XElement magid = userElement.Element("magid");
            }
        }
    }
}
