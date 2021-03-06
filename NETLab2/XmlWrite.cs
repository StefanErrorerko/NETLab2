using System;
using System.Text;
using NET_Lab2.Entity;
using System.Xml;

namespace NET_Lab2
{
    internal class XmlWrite
    {
        private readonly XmlWriterSettings settings = new XmlWriterSettings() 
        { 
            Indent = true, Encoding = Encoding.UTF8 
        };

        internal void CreateXml(Data data)
        {
            using (XmlWriter writer = XmlWriter.Create("authors.xml", settings))
            {
                writer.WriteStartElement("authors");
                foreach (Author author in data.Authors)
                {
                    writer.WriteStartElement("author");
                    writer.WriteElementString("authorid", author.AuthorId.ToString());
                    writer.WriteElementString("surname", author.Surname);
                    writer.WriteElementString("name", author.Name);
                    writer.WriteElementString("secondname", author.Secondname);
                    writer.WriteElementString("organisation", author.Organ);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            using (XmlWriter writer = XmlWriter.Create("magazines.xml", settings))
            {
                writer.WriteStartElement("magazines");
                foreach (Magazine magazine in data.Mags)
                {
                    writer.WriteStartElement("magazine");
                    writer.WriteElementString("magid", magazine.MagId.ToString());
                    writer.WriteElementString("name", magazine.Name);
                    writer.WriteElementString("established", magazine.Est.ToString());
                    writer.WriteElementString("circulation", magazine.Circ.ToString());
                    writer.WriteElementString("frequency", magazine.Freq.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            using (XmlWriter writer = XmlWriter.Create("articles.xml", settings))
            {
                writer.WriteStartElement("articles");
                foreach (Article article in data.Articles)
                {
                    writer.WriteStartElement("article");
                    writer.WriteElementString("articleid", article.ArticleId.ToString());
                    writer.WriteElementString("name", article.Name);
                    writer.WriteElementString("authorid", article.AuthorId.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            using (XmlWriter writer = XmlWriter.Create("editordocuments.xml", settings))
            {
                writer.WriteStartElement("docs");
                foreach (EditorDoc doc in data.Docs)
                {
                    writer.WriteStartElement("doc");
                    writer.WriteElementString("docid", doc.DocId.ToString());
                    writer.WriteElementString("date", doc.Date.ToString());
                    writer.WriteElementString("articleid", doc.ArticleId.ToString());
                    writer.WriteElementString("magid", doc.MagId.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }
    }
}
