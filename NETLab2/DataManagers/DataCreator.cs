using System;
using NET_Lab2.Entities;
using NET_Lab2.Instruments;
using System.Collections.Generic;

namespace NET_Lab2.DataManagers
{
    public class DataCreator
    {
        public Author CreateAuthor(string authorData, int authorsCount)
        {
            var stingData = authorData.Split(',');

            var author = new Author()
            {
                AuthorId = authorsCount + 1,
                Surname = stingData[0],
                Name = stingData[1],
                Secondname = stingData[2],
                Organ = stingData[3]
            };
            return author;
        }
        public Magazine CreateMag(string magData, int magCount)
        {
            var stingData = magData.Split(',');

            var est = Convert.ToDateTime(stingData[1]);
            DateTimeAccuracyMonitor.CheckDate(est);
            var mag = new Magazine()
            {
                MagId = magCount + 1,
                Name = stingData[0],
                Est = est,
                Circ = int.Parse(stingData[2]),
                Freq = Convert.ToDouble(stingData[3])
            };
            return mag;
        }
        public Article CreateArticle(string articleData, int articleCount)
        
        {
            var stingData = articleData.Split(',');
            var authorid = int.Parse(stingData[1]);

            if(!isIndexInBounds(authorid, articleCount+1))
            {
                return null;
            }

            var article = new Article()
            {
                ArticleId = articleCount +1,
                Name = stingData[0],
                AuthorId = authorid
            };
            return article;
        }

        public List<EditorDoc> GenerateDocs(Data data)
        {
            var docs = new List<EditorDoc>();
            for (int articleid = 0; articleid < data.Articles.Count; articleid++)
            {
                foreach (var doc in GenerateDocsForArticle(data, articleid))
                {
                    docs.Add(doc);
                }
            }
            return docs;
        }
        private List<EditorDoc> GenerateDocsForArticle(Data data, int articleId)
        {
            var docs = new List<EditorDoc>();
            var rnd = new Random();
            DateTime date;
            Magazine mag;
            // a count of publishing of each article
            var article = data.Articles[articleId];
            var publishCount = rnd.Next(0, 4);
            for (int j = 0; j < publishCount; j++)
            {
                // gets a random mag
                mag = data.Mags[rnd.Next(0, data.Mags.Count)];

                // a random date
                var start = mag.Est;
                var range = (DateTime.Now - start).Days;
                date = start.AddDays(rnd.Next(range));
                DateTimeAccuracyMonitor.CheckDate(date);

                var id = data.Docs.Count + 1;
                var doc = new EditorDoc()
                {
                    DocId = id,
                    Date = date,
                    ArticleId = article.ArticleId,
                    MagId = mag.MagId
                };
                docs.Add(doc);
            }
            return docs;
        }

        private bool isIndexInBounds(int index, int highbound, int lowbound = 1)
        {
            return (index >= lowbound && index <= highbound) ? true : false;
        }
    }
}
