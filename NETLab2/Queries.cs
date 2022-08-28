using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using NET_Lab2.Extensions;
using NET_Lab2.Entities;
using NET_Lab2.Instruments;

namespace NET_Lab2
{
    internal static class Queries
    {
        public static XDocument XmlAuthors;
        public static XDocument XmlArticles;
        public static XDocument XmlMags;
        public static XDocument XmlDocs;

        //1 переп
        public static IEnumerable<Magazine> GetMags()
        {
            return XmlMags.Descendants("magazine")
                .Select(mag => mag.ToMagazine());
        }

        // 2 
        public static Dictionary<string, DateTime> GetMagsNameEtEstbl()
        {
            return
            XmlMags.Descendants("magazine").Select(mag => new
            {
                Name = mag.Element("name").Value,
                Established = mag.Element("established").Value
            }).ToDictionary(mags => mags.Name, mags => Convert.ToDateTime(mags.Established));
        }

        //3
        public static IEnumerable<Magazine> GetMagsWithLowCirc()
        {
            return XmlMags.Descendants("magazine")
                .Where(mag => Int32.Parse(mag.Element("circulation").Value) < 5000)
                .Select(mag => mag.ToMagazine());
        }

        //4 
        public static IEnumerable<Article> GetArticlesU2014()
        {
            return from articles in XmlArticles.Descendants("article")
                   join docs in XmlDocs.Descendants("doc") 
                       on articles.Element("articleid").Value equals docs.Element("articleid").Value
                   where Convert.ToDateTime(docs.Element("date").Value).Year < 2014
                   select articles.ToArticle();
        }

        //5
        public static Dictionary<string, double> GetMagsFreqU2()
        {
            return (from mags in XmlMags.Descendants("magazine")
                    where Convert.ToDouble(mags.Element("frequency").Value) < 2
                    select new
                    {
                        Name = mags.ToMagazine().Name,
                        Frequency = mags.Element("frequency").Value
                    }).ToDictionary(a => a.Name, a => Convert.ToDouble(a.Frequency));
        }

        //6
        public static IEnumerable<Magazine> GetMagFirstOfIndependentUA()
        {
            return (from mags in XmlMags.Descendants("magazine")
                    join docs in XmlDocs.Descendants("doc")
                        on mags.Element("magid") equals docs.Element("magid")
                    where Convert.ToDateTime(mags.Element("established").Value).Year > 1991
                    select mags.ToMagazine());
        }

        //7
        public static ILookup<Magazine, IEnumerable<Article>> GetMagsAndArticles()
        {
            var mags = XmlMags.Descendants("magazine");
            var docs = XmlDocs.Descendants("doc");
            var art = XmlArticles.Descendants("article");

            var q1 = (from m in mags
                      join d in docs
                        on m.Element("magid").Value equals d.Element("magid").Value
                      join ar in art
                        on d.Element("articleid").Value equals ar.Element("articleid").Value
                        into temp
                      select new
                      {
                          Mag = m.ToMagazine(),
                          Art = from article in temp 
                                select article.ToArticle()
                      }).ToLookup(a => a.Mag, a => a.Art);
            return q1;
        }

        //8 переп
        public static IDictionary<int, IGrouping<int, Article>> GetAuthorsAndItsArticles()
        {
            var q = (from articles in XmlArticles.Descendants("article")
                     join authors in XmlAuthors.Descendants("author")
                         on articles.Element("articleid") equals authors.Element("articleid")
                     join docs in XmlDocs.Descendants("doc")
                         on articles.Element("articleid") equals docs.Element("articleid")
                         into temp
                     group articles.ToArticle() by temp.Count()
                        into g
                     select new
                     {
                         v1 = g.Key,
                         v2 = g
                     }).ToDictionary(amount => amount.v1, author => author.v2);
            return q;






            //return (from authors in XmlAuthors.Descendants("author")
            //        join articles in XmlArticles.Descendants("article")
            //            on authors.Element("authorid").Value equals articles.Element("authorid").Value
            //            into temp
            //        group authors.ToAuthor() by temp.Count() into g
            //        select new 
            //        { 
            //            v1 = g.Key, v2 = g 
            //        })
            //        .ToLookup(a => a.v1, a => a.v2);

        }

        //9
        public static Dictionary<Magazine, double> GetMagsAndCirc()
        {
            return (XmlMags.Descendants("magazine").Select(mag => new {
                Mag = mag.ToMagazine(),
                Amount = 12 *
                Convert.ToDouble(mag.Element("circulation").Value) *
                Convert.ToDouble(mag.Element("frequency").Value)
            })).ToDictionary(mags => mags.Mag, mags => mags.Amount);
        }

        public static double GetCircSummary()
        {
            return GetMagsAndCirc().Sum(mag => mag.Value);
        }

        //10
        public static IEnumerable<IGrouping<int, Article>> GetArticlesGroupByPublish()
        {
            return from article in XmlArticles.Descendants("article")
                   join doc in XmlDocs.Descendants("doc")
                       on article.Element("articleid").Value equals doc.Element("articleid").Value
                       into temp
                   group article.ToArticle() by temp.Count();
        }

        //11
        public static Dictionary<int, IGrouping<int, EditorDoc>> GetArticlesGroupByYearOver2002()
        {
            return (from article in XmlArticles.Descendants("article")
                    join doc in XmlDocs.Descendants("doc")
                        on article.Element("articleid").Value equals doc.Element("articleid").Value
                    orderby Convert.ToDateTime(doc.Element("date").Value).Year
                    group doc.ToEditorDoc() by Convert.ToDateTime(doc.Element("date").Value).Year
                        into g
                    where g.Any(x => x.Date.Year > 2002)

                    select new
                    {
                        Key = g.Key,
                        Docs = g
                    }).ToDictionary(d => d.Key, d => d.Docs);

        }

        //12
        public static IEnumerable<Article> GetArticlesInPotopMag()
        {
            return from article in XmlArticles.Descendants("article")
                   where (
                         (from doc in XmlDocs.Descendants("doc")
                          join mag in (
                             from mag2 in XmlMags.Descendants("magazine")
                             where mag2.Name == "Potop"
                             select mag2)
                                 on doc.Element("magid").Value equals mag.Element("magid").Value
                          select doc.Element("articleid").Value).Contains(article.Element("articleid").Value)
                          )
                   select article.ToArticle();
        }

        //13 переп
        public static IEnumerable<Magazine> GetConcatedLists()
        {
            return XmlMags.Elements("magazines").Take(1)
                .Concat(XmlMags.Elements("magazines")).TakeLast(1)
                .Select(a => a.Element("magazine").ToMagazine());
        }


        // 14 переп
        public static IEnumerable<Article> GetOrderedArticles()
        {
            return XmlArticles.Descendants("article").Select(a => a.ToArticle()).OrderBy(a => a);
        }

        //15 переп

        public static IEnumerable<Author> GetUnpublichedAuthors()
        {
            var q = from au in XmlAuthors.Descendants("author")
                    join ar in XmlArticles.Descendants("article")
                        on au.Element("authorid").Value equals ar.Element("authorid").Value
                    select au;

            return XmlAuthors.Descendants("author").Except(q).Select(a => a.ToAuthor());
        }
    }
}
