using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using NET_Lab2.Entity;
using NET_Lab2.Instruments;

namespace NET_Lab2
{
    internal static class Queries
    {
        public static XDocument Xmlauthors;
        public static XDocument Xmlarticles;
        public static XDocument Xmlmags;
        public static XDocument Xmldocs;

        //1
        public static IEnumerable<string> GetMags()
        {
            return Xmldocs.Descendants("magazine")
                .Select(magNode => magNode.Element("name").Value);
        }

        // 2 
        public static IEnumerable<string> GetAuthorsBySurname()
        {

            return Xmlauthors.Descendants("author")
               .Select(p => p.Element("surname").Value)
               .OrderBy(p => p);
        }

        //3
        public static IEnumerable<string> GetMagsWithLowCirc()
        {
            return Xmlmags.Descendants("magazine")
                .Where(magNode => Int32.Parse(magNode.Element("circulation").Value) < 5000)
                .Select(magNode => magNode.Element("name").Value);
        }

        //4
        public static ILookup<string, string> GetArticlesU2014()
        {
            return (from xe1 in Xmldocs.Descendants("doc")
                    join xe2 in Xmlarticles.Descendants("article") on xe1.Element("articleid").Value equals xe2.Element("articleid").Value
                    where Convert.ToDateTime(xe1.Element("date").Value) < new DateTime(2014, 01, 01)
                    select new
                    {
                        Name = xe2.Element("name").Value,
                        Author = xe2.Element("authorid").Value,
                    }).ToLookup(a => a.Name, a => a.Author);
        }

        //5
        public static Dictionary<string, string> GetMagsFreqU1()
        {
            return (from xe in Xmlmags.Descendants("magazine")
                    where Convert.ToDouble(xe.Element("frequency").Value) < 1
                    select new
                    {
                        Name = xe.Element("name").Value,
                        Frequency = xe.Element("frequency").Value
                    }).ToDictionary(a => a.Name, a => a.Frequency);
        }

        //6
        public static IEnumerable<char> GetMagFirstOfIndependentUA()
        {
            return (from x in Xmlmags.Descendants("magazine")
                    select x.Element("name").Value
                    .FirstOrDefault(a => Convert.ToDateTime(a).Year <= 1991));
        }

        //7
        public static Dictionary<string, IEnumerable<XElement>> GetMagsAndItsArticles()
        {
            var mags = Xmlmags.Descendants("magazine");
            var docs = Xmldocs.Descendants("doc");

            var q1 = (from m in mags
                      join d in docs
                        on m.Element("magid").Value equals d.Element("magid").Value
                        into temp
                      select new
                      {
                          Mag = m.Element("name").Value,
                          Art = temp
                      }).ToDictionary(a => a.Mag, a => a.Art);

            return q1;
        }

        //8
        public static ILookup<int, IGrouping<int, XElement>> GetAuthorsAndItsArticles()
        {
            return (from xe1 in Xmlarticles.Descendants("article")
                    join xe2 in Xmldocs.Descendants("doc")
                    on xe1.Element("articleid").Value equals xe2.Element("articleid").Value
                    into temp
                    group xe1 by temp.Count() into g
                    select new { v1 = g.Key, v2 = g })
                    .ToLookup(a => a.v1, a => a.v2);

        }

        //9
        public static Dictionary<string, double> GetMagsAndCirc()
        {
            return (Xmlmags.Descendants("magazine").Select(x => new {
                Mag = (string)x,
                Amount = 12 *
                Convert.ToDouble(x.Element("circulation").Value) *
                Convert.ToDouble(x.Element("frequency").Value)
            })).ToDictionary(mags => mags.Mag, mags => mags.Amount);
        }

        public static double GetCircSummary()
        {
            return GetMagsAndCirc().Sum(x => x.Value);
        }

        //10
        public static IEnumerable<IGrouping<int, XElement>> GetArticlesGroupByPublish()
        {
            return from ar in Xmlarticles.Descendants("article")
                   join d in Xmldocs.Descendants("doc")
                   on ar.Element("articleid").Value equals d.Element("articleid").Value
                   into temp
                   group ar by temp.Count();
        }

        //11
        public static Dictionary<int, IGrouping<int, XElement>> GetArticlesGroupByYearOver2002()
        {
            return (from a in Xmlarticles.Descendants("article")
                    join d in Xmldocs.Descendants("doc")
                    on a.Element("articleid").Value equals d.Element("articleid").Value
                    orderby Convert.ToDateTime(d.Element("date").Value).Year
                    group d by Convert.ToDateTime(d.Element("date").Value).Year into g
                    where g.Any(x => Convert.ToDateTime(x.Element("date").Value).Year > 2002)

                    select new
                    {
                        Key = g.Key,
                        Values = g,
                    }).ToDictionary(d => d.Key, d => d.Values);

        }

        //12
        public static IEnumerable<string> GetArticlesInPotopMag()
        {
            return from ar in Xmlarticles.Descendants("article")
                   where (
                         (from d in Xmldocs.Descendants("doc")
                          join m in (
                             from m2 in Xmlmags.Descendants("magazine")
                             where m2.Name == "Potop"
                             select m2)
                                 on d.Element("magid").Value equals m.Element("magid").Value
                          select d.Element("articleid").Value).Contains(ar.Element("articleid").Value)
                          )
                   select ar.Element("name").Value;
        }

        //13
        //public IEnumerable<Author> GetConcatedLists(List<Author> au1, List<Author> au2)
        //{
        //    Xmlmags.Elements("magazine").
        //        .Concat(Xmlmags.Elements("magzine")
        //        .LastOrDefault()).Select(bus => bus.Element("number").Value);
        //}

        //public static IEnumerable<Author> GetDistinctedUnionList(List<Author> au1, List<Author> au2)
        //{
        //    return au1.Union(au2).Distinct(
        //        new AuthorEqualityComparer());
        //}

        ////14 
        //public static IEnumerable<Author> GetDifferneceBetweenLists(List<Author> au1, List<Author> au2)
        //{
        //    return au1.Except(au2, new AuthorEqualityComparer());
        //}

        ////15
        //public static IEnumerable<Author> GetIntersectBetweenLists(List<Author> au1, List<Author> au2)
        //{
        //    return au1.Intersect(au2, new AuthorEqualityComparer());
        //}
    }
}
