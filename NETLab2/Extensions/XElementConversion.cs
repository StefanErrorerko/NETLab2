using System;
using System.Xml.Linq;
using NET_Lab2.Entities;

namespace NET_Lab2.Extensions
{
    public static class XElementConversion
    {
        public static Article ToArticle(this XElement data)
        {
            return new Article
            {
                ArticleId = int.Parse(data.Element("articleid").Value),
                AuthorId = int.Parse(data.Element("authorid").Value),
                Name = data.Element("name").Value
            };
        }

        public static Author ToAuthor(this XElement data)
        {
            return new Author
            {
                AuthorId = int.Parse(data.Element("authorid").Value),
                Surname = data.Element("surname").Value,
                Name = data.Element("name").Value,
                Secondname = data.Element("secondname").Value,
                Organ = data.Element("organisation").Value
            };
        }

        public static Magazine ToMagazine(this XElement data)
        {
            return new Magazine
            {
                MagId = int.Parse(data.Element("magid").Value),
                Name = data.Element("name").Value,
                Est = Convert.ToDateTime(data.Element("established").Value),
                Circ = int.Parse(data.Element("circulation").Value),
                Freq = double.Parse(data.Element("frequency").Value)
            };
        }

        public static EditorDoc ToEditorDoc(this XElement data)
        {
            return new EditorDoc
            {
                DocId = int.Parse(data.Element("articleid").Value),
                Date = Convert.ToDateTime(data.Element("date").Value),
                ArticleId = int.Parse(data.Element("articleid").Value),
                MagId = int.Parse(data.Element("magid").Value)
            };
        }
    }
}
