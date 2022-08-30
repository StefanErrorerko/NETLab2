using NET_Lab2.DataManagers;
using System.Text;
using System.Xml;
using NET_Lab2.Constants;

namespace NET_Lab2.XmlProcessors
{
    public class WriterXml
    {
        private readonly XmlWriterSettings _settings = new XmlWriterSettings()
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
            using var writer = XmlWriter.Create(FileConstant.Author, _settings);
            writer.WriteStartElement(TagConstant.Authors);
            foreach (var author in data.Authors)
            {
                writer.WriteStartElement(TagConstant.Author);
                writer.WriteElementString(TagConstant.AuthorId, author.AuthorId.ToString());
                writer.WriteElementString(TagConstant.Surname, author.Surname);
                writer.WriteElementString(TagConstant.Name, author.Name);
                writer.WriteElementString(TagConstant.Secondname, author.Secondname);
                writer.WriteElementString(TagConstant.Organisation, author.Organ);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        private void CreateArticleXml(Data data)
        {
            using var writer = XmlWriter.Create(FileConstant.Article, _settings);
            writer.WriteStartElement(TagConstant.Articles);
            foreach (var article in data.Articles)
            {
                writer.WriteStartElement(TagConstant.Article);
                writer.WriteElementString(TagConstant.ArticleId, article.ArticleId.ToString());
                writer.WriteElementString(TagConstant.Name, article.Name);
                writer.WriteElementString(TagConstant.AuthorId, article.AuthorId.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        private void CreateMagazineXml(Data data)
        {
            using var writer = XmlWriter.Create(FileConstant.Magazine, _settings);
            writer.WriteStartElement(TagConstant.Magazines);
            foreach (var magazine in data.Mags)
            {
                writer.WriteStartElement(TagConstant.Magazine);
                writer.WriteElementString(TagConstant.MagazineId, magazine.MagId.ToString());
                writer.WriteElementString(TagConstant.Name, magazine.Name);
                writer.WriteElementString(TagConstant.Established, magazine.Est.ToString());
                writer.WriteElementString(TagConstant.Circulation, magazine.Circ.ToString());
                writer.WriteElementString(TagConstant.Frequency, magazine.Freq.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        private void CreateDocXml(Data data)
        {
            using var writer = XmlWriter.Create(FileConstant.EditorDocument, _settings);
            writer.WriteStartElement(TagConstant.Docs);
            foreach (var doc in data.Docs)
            {
                writer.WriteStartElement(TagConstant.Doc);
                writer.WriteElementString(TagConstant.DocId, doc.DocId.ToString());
                writer.WriteElementString(TagConstant.Date, doc.Date.ToString());
                writer.WriteElementString(TagConstant.ArticleId, doc.ArticleId.ToString());
                writer.WriteElementString(TagConstant.MagazineId, doc.MagId.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
