using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using NET_Lab2.Extensions;
using NET_Lab2.Entities;
using NET_Lab2.Instruments;
using NET_Lab2.XmlProcessors;
using NET_Lab2.Constants;

namespace NET_Lab2.QueryContainers
{
    public class Queries
    {
        public XDocument XmlAuthors;
        public XDocument XmlArticles;
        public XDocument XmlMags;
        public XDocument XmlDocs;

        public Queries(ReaderXml readXml)
        {
            XmlAuthors = readXml.XmlAuthors;
            XmlMags = readXml.XmlMags;
            XmlArticles = readXml.XmlArticles;
            XmlDocs = readXml.XmlDocs;
        }

        //1 
        public IEnumerable<Article> GetArticlesUnpublished()
        {
            return from articles in XmlArticles.Descendants(TagConstant.Article)
                   where !((from docs in XmlDocs.Descendants(TagConstant.Doc)
                           select docs.Element(TagConstant.ArticleId).Value)
                           .Contains(articles.Element(TagConstant.ArticleId).Value))
                   select articles.ToArticle();
        }

        // 2 
        public Dictionary<string, DateTime> GetMagsNameAndEstbl()
        {
            return XmlMags.Descendants(TagConstant.Magazine).Select(mag => new
            {
                Name = mag.Element(TagConstant.Name).Value,
                Established = mag.Element(TagConstant.Established).Value
            }).ToDictionary
            (
                mag => mag.Name, 
                mag => Convert.ToDateTime(mag.Established)
            );
        }

        //3
        public IEnumerable<Magazine> GetMagsWithCirc(int circ)
        {
            return XmlMags.Descendants(TagConstant.Magazine)
                .Where(mag => Int32.Parse(mag.Element(TagConstant.Circulation).Value) < circ)
                .Select(mag => mag.ToMagazine());
        }

        //4 
        public IEnumerable<Article> GetArticlesInBounds(DateTime highbound)
        {
            return (from articles in XmlArticles.Descendants(TagConstant.Article)
                   join docs in XmlDocs.Descendants(TagConstant.Doc) 
                       on articles.Element(TagConstant.ArticleId).Value 
                        equals docs.Element(TagConstant.ArticleId).Value
                   where 
                        Convert.ToDateTime(docs.Element(TagConstant.Date).Value) < highbound
                   select articles.ToArticle()).Distinct();
        }

        //5
        public Dictionary<string, double> GetMagsFreqInBounds(double lowbound, double highbound)
        {
            return (from mags in XmlMags.Descendants(TagConstant.Magazine)
                    where lowbound < Convert.ToDouble(mags.Element(TagConstant.Frequency).Value)
                        && Convert.ToDouble(mags.Element(TagConstant.Frequency).Value) < highbound
                    select new
                    {
                        Name = mags.ToMagazine().Name,
                        Frequency = mags.Element(TagConstant.Frequency).Value
                    }).ToDictionary
                    (
                        mag => mag.Name,
                        mag => Convert.ToDouble(mag.Frequency)
                    );
        }

        //6
        public Magazine GetMagFirstInBounds(DateTime highbound)
        {
            return (from mags in XmlMags.Descendants(TagConstant.Magazine)
                    orderby 
                        Convert.ToDateTime(mags.Element(TagConstant.Established).Value).Year
                    select mags.ToMagazine())
                        .FirstOrDefault(mag => (mag.Est <= highbound));
        }

        //7
        public ILookup<Magazine, IEnumerable<Article>> GetMagsAndArticles()
        {
            var mags = XmlMags.Descendants(TagConstant.Magazine);
            var docs = XmlDocs.Descendants(TagConstant.Doc);
            var articles = XmlArticles.Descendants(TagConstant.Article);

            return (from m in mags
                      join d in docs
                        on m.Element(TagConstant.MagazineId).Value 
                            equals d.Element(TagConstant.MagazineId).Value
                        into temp
                      select new
                      {
                          Mag = m.ToMagazine(),
                          Art = (
                            from doc in temp
                            join art in XmlArticles.Descendants(TagConstant.Article)
                                on doc.Element(TagConstant.ArticleId).Value
                                    equals art.Element(TagConstant.ArticleId).Value
                            select art.ToArticle()
                            )

                      }).ToLookup(mag => mag.Mag, article => article.Art);
        }

        //8 
        public ILookup<string, Article> GetAuthorsAndItsArticles()
        {
            return (from authors in XmlAuthors.Descendants(TagConstant.Author)
                    join articles in XmlArticles.Descendants(TagConstant.Article) 
                        on authors.Element(TagConstant.AuthorId).Value 
                            equals articles.Element(TagConstant.AuthorId).Value 
                        into temp
                    from t in temp.DefaultIfEmpty()
                    select new 
                    { 
                        Author = authors.ToAuthor().ToString(), 
                        Article = ((t == null) ? null : t.ToArticle()) 
                    })
         .ToLookup(au => au.Author, art => art.Article);
        }

        //9
        public Dictionary<Magazine, double> GetMagsWithYearCirc()
        {
            return (XmlMags.Descendants(TagConstant.Magazine).Select(mag => new {
                Mag = mag.ToMagazine(),
                Amount = 12 *
                Convert.ToDouble(mag.Element(TagConstant.Circulation).Value) *
                Convert.ToDouble(mag.Element(TagConstant.Frequency).Value)
            })).ToDictionary(mag => mag.Mag, mag => mag.Amount);
        }

        public double GetCircSummary()
        {
            return GetMagsWithYearCirc().Sum(mag => mag.Value);
        }

        //10
        public IEnumerable<IGrouping<int, Article>> GetArticlesGroupByPublish()
        {
            return from article in XmlArticles.Descendants(TagConstant.Article)
                   join doc in XmlDocs.Descendants(TagConstant.Doc)
                       on article.Element(TagConstant.ArticleId).Value 
                        equals doc.Element(TagConstant.ArticleId).Value
                       into temp
                   group article.ToArticle() by temp.Count()
                    into g
                   select g;
        }

        //11
        public Dictionary<int, IEnumerable<EditorDoc>> GetArticlesGroupByYear()
        {
            return (from article in XmlArticles.Descendants(TagConstant.Article)
                    join doc in XmlDocs.Descendants(TagConstant.Doc)
                        on article.Element(TagConstant.ArticleId).Value 
                            equals doc.Element(TagConstant.ArticleId).Value
                    orderby Convert.ToDateTime(doc.Element(TagConstant.Date).Value).Year
                    group doc.ToEditorDoc() 
                        by Convert.ToDateTime(doc.Element(TagConstant.Date).Value).Year
                        into g
                    select g
                    ).ToDictionary(g => g.Key, g => g.Select(doc => doc));
        }

        //12
        public IEnumerable<Article> GetArticlesInMag(string magName)
        {
            return from article in XmlArticles.Descendants(TagConstant.Article)
                   where (
                         (from doc in XmlDocs.Descendants(TagConstant.Doc)
                          join mag in (
                             from mag2 in XmlMags.Descendants(TagConstant.Magazine)
                             where mag2.Element(TagConstant.Name).Value.Equals(magName)
                             select mag2)
                                 on doc.Element(TagConstant.MagazineId).Value 
                                    equals mag.Element(TagConstant.MagazineId).Value
                          select doc.Element(TagConstant.ArticleId).Value)
                          .Contains(article.Element(TagConstant.ArticleId).Value))
                   select article.ToArticle();
        }

        //13 
        public IEnumerable<Author> GetAuthorsExceptedWriterOfArticle(string articleName)
        {
            return (from authors in XmlAuthors.Descendants(TagConstant.Author)
                    select authors.ToAuthor())
                   .Except(
                       from authors2 in XmlAuthors.Descendants(TagConstant.Author)
                       join article in XmlArticles.Descendants(TagConstant.Article)
                           on authors2.Element(TagConstant.AuthorId).Value
                               equals article.Element(TagConstant.AuthorId).Value
                       where article.Element(TagConstant.Name).Value.Equals(articleName)
                       select authors2.ToAuthor());
        }

        // 14
        public IEnumerable<EditorDoc> GetFirstAndLastDoc()
        {
            var orderedDocs = XmlDocs.Descendants(TagConstant.Doc)
                .OrderBy(doc => Convert.ToDateTime(doc.Element(TagConstant.Date).Value))
                .Select(doc => doc.ToEditorDoc());

            return orderedDocs.Take(1)
                .Concat(orderedDocs.TakeLast(1))
                .Select(doc => doc);
        }

        //15 
        public IEnumerable<Author> GetAuthorsInTwoMags(string magName1, string magName2)
        {
            // doucments about publishing an article in magazines magName1 and magName2
            var docsNeeded = DocsAboutTwoMags(magName1, magName2);

            // articles, that documents above contain
            var articlesNeeded = ArticlesInTwoMags(docsNeeded);

            // authors of articles above
            return AuthorsInTwoMags(articlesNeeded);
        }

        private IEnumerable<XElement> DocsAboutTwoMags(string magName1, string magName2)
        {
            return from docs in XmlDocs.Descendants(TagConstant.Doc)
                   join mags in XmlMags.Descendants(TagConstant.Magazine)
                      on docs.Element(TagConstant.MagazineId).Value
                          equals mags.Element(TagConstant.MagazineId).Value
                   where mags.Element(TagConstant.Name).Value.Equals(magName1) ||
                      mags.Element(TagConstant.Name).Value.Equals(magName2)
                   select docs;
        }

        private IEnumerable<XElement> ArticlesInTwoMags(IEnumerable<XElement> docsNeeded)
        {
            return from articles in XmlArticles.Descendants(TagConstant.Article)
                   join docs in docsNeeded
                      on articles.Element(TagConstant.ArticleId).Value
                          equals docs.Element(TagConstant.ArticleId).Value
                   select articles;
        }

        private IEnumerable<Author> AuthorsInTwoMags(IEnumerable<XElement> articlesNeeded)
        {
            return (from authors in XmlAuthors.Descendants(TagConstant.Author)
             join articles in articlesNeeded
                 on authors.Element(TagConstant.AuthorId).Value
                     equals articles.Element(TagConstant.AuthorId).Value
             select authors.ToAuthor()).Distinct();
        }
    }
}
