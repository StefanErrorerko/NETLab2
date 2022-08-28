using System.Xml.Linq;

namespace NET_Lab2
{
    internal class XmlRead
    {
        internal readonly XDocument XmlAuthors;
        internal readonly XDocument XmlArticles;
        internal readonly XDocument XmlMags;
        internal readonly XDocument XmlDocs;

        internal XmlRead()
        {
            XmlAuthors = XDocument.Load("authors.xml");
            //foreach (var userElement in XmlAuthors.Element("authors").Elements("author"))
            //{
            //    var authorId = userElement.Element("authorid");
            //    var surname = userElement.Element("surname");
            //    var name = userElement.Element("name");
            //    var secondname = userElement.Element("secondname");
            //    var organisation = userElement.Element("organisation");
            //}

            XmlMags = XDocument.Load("magazines.xml");
            //foreach (var userElement in XmlMags.Element("magazines").Elements("magazine"))
            //{
            //    var magId = userElement.Element("magid");
            //    var name = userElement.Element("name");
            //    var established = userElement.Element("established");
            //    var circulation = userElement.Element("circulation");
            //    var frequency = userElement.Element("frequency");
            //}

            XmlArticles = XDocument.Load("articles.xml");
            //foreach (var userElement in XmlArticles.Element("articles").Elements("article"))
            //{
            //    var articleId = userElement.Element("articleid");
            //    var name = userElement.Element("name");
            //    var author = userElement.Element("authorid");
            //}

            XmlDocs = XDocument.Load("editordocuments.xml");
            //foreach (var userElement in XmlDocs.Element("docs").Elements("doc"))
            //{
            //    var docId = userElement.Element("docid");
            //    var date = userElement.Element("date");
            //    var articleId = userElement.Element("articleid");
            //    var magid = userElement.Element("magid");
            //}
        }
    }
}
