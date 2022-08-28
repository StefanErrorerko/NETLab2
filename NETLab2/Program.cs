using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using NET_Lab2.Entities;
using NET_Lab2.Instruments;


namespace NET_Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            Data data = new Data();
            XmlWrite writeXml = new XmlWrite();

            Console.OutputEncoding = Encoding.UTF8;

            data.Articles = new List<Article> { };
            data.Mags = new List<Magazine> { };
            data.Authors = new List<Author> { };
            data.Docs = new List<EditorDoc> { };
            data.Au1 = new List<Author> { };
            data.Au2 = new List<Author> { };

            ConsoleViewer.data = data;

            ConsoleViewer.DisplayCreateAuthor();
            ConsoleViewer.DisplayCreateMagazine();
            ConsoleViewer.DisplayCreateArticle();
            ConsoleViewer.DisplayCreateDoc();
            ConsoleViewer.DisplayAllCreated();

            writeXml.CreateXml(data);
            XmlRead readXml = new XmlRead();
            Queries.XmlAuthors = readXml.XmlAuthors;
            Queries.XmlMags = readXml.XmlMags;
            Queries.XmlArticles = readXml.XmlArticles;
            Queries.XmlDocs = readXml.XmlDocs;

            ConsoleViewer.ShowMags();
            ConsoleViewer.ShowMagsEtEstbl();
            ConsoleViewer.ShowMagsWithNormalCirc();
            ConsoleViewer.ShowArticlesU2014();
            ConsoleViewer.ShowMagsFreqU2();
            ConsoleViewer.ShowFirstMagOfIndependentUA();
            ConsoleViewer.ShowMagsAndItsArticles();
            ConsoleViewer.ShowAuthorsAndItsArticles();
            ConsoleViewer.ShowCircSummary();
            ConsoleViewer.ShowArticlesGroupByPublish();
            ConsoleViewer.ShowArticlesGroupByYearOver2002();
            ConsoleViewer.ShowArticlesInPotopMag();
            //ConsoleViewer.ShowConcatedLists();
            //ConsoleViewer.ShowOrderedArticles();
            //ConsoleViewer.ShowUnpublishedAuthors();
        }
    }
}
