using System.Text;
using System.Xml;

namespace NET_Lab2
{
    public class WriterXml
    {
        private readonly XmlWriterSettings settings = new XmlWriterSettings()
        {
            Indent = true,
            Encoding = Encoding.UTF8
        };

        public void CreateXml(Data data)
        {
            CreateAuthorXml(data);
            CreateArticleXml(data);
            CreateMagazineXml(data);
            CreateDocXml(data);
        }

        private void CreateAuthorXml(Data data)
        {
            using (var writer = XmlWriter.Create("authors.xml", settings))
            {
                writer.WriteStartElement("authors");
                foreach (var author in data.Authors)
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
        }
        private void CreateArticleXml(Data data)
        {
            using (var writer = XmlWriter.Create("articles.xml", settings))
            {
                writer.WriteStartElement("articles");
                foreach (var article in data.Articles)
                {
                    writer.WriteStartElement("article");
                    writer.WriteElementString("articleid", article.ArticleId.ToString());
                    writer.WriteElementString("name", article.Name);
                    writer.WriteElementString("authorid", article.AuthorId.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }
        private void CreateMagazineXml(Data data)
        {
            using (var writer = XmlWriter.Create("magazines.xml", settings))
            {
                writer.WriteStartElement("magazines");
                foreach (var magazine in data.Mags)
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
        }
        private void CreateDocXml(Data data)
        {
            using (var writer = XmlWriter.Create("editordocuments.xml", settings))
            {
                writer.WriteStartElement("docs");
                foreach (var doc in data.Docs)
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
