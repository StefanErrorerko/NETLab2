using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using NET_Lab2.Entity;
using NET_Lab2.Instruments;


namespace NET_Lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            Data data = new Data();
            XmlWrite writexml = new XmlWrite();

            Console.OutputEncoding = Encoding.UTF8;

            data.Articles = new List<Article> { };
            data.Mags = new List<Magazine> { };
            data.Authors = new List<Author> { };
            data.Docs = new List<EditorDoc> { };
            data.au1 = new List<Author> { };
            data.au2 = new List<Author> { };

            ConsoleViewer.data = data;

            ConsoleViewer.DisplayCreateAuthor();
            ConsoleViewer.DisplayCreateMagazine();
            ConsoleViewer.DisplayCreateArticle();
            ConsoleViewer.DisplayCreateDoc();
            ConsoleViewer.DisplayAllCreated();

            //====================================================================================================================
            //===================================================Запис у файл=====================================================
            //====================================================================================================================

            writexml.CreateXml(data);
            XmlRead readxml = new XmlRead();
            Queries.Xmlauthors = readxml.Xmlauthors;
            Queries.Xmlmags = readxml.Xmlmags;
            Queries.Xmlarticles = readxml.Xmlarticles;
            Queries.Xmldocs = readxml.Xmldocs;

            ConsoleViewer.ShowMags();
            ConsoleViewer.ShowMagsEtEstbl();
            ConsoleViewer.ShowMagsWithNormalCirc();
            ConsoleViewer.ShowArticlesU2014();
            ConsoleViewer.ShowMagsFreqU1();
            ConsoleViewer.ShowFirstMagOfIndependentUA();
            ConsoleViewer.ShowMagsAndItsArticles();
            ConsoleViewer.ShowAuthorsAndItsArticles();
            ConsoleViewer.ShowCircSummary();
            ConsoleViewer.ShowArticlesGroupByPublish();
            ConsoleViewer.ShowArticlesGroupByYearOver2002();
            ConsoleViewer.ShowArticlesInPotopMag();
            ConsoleViewer.ShowConcatedLists();
            ConsoleViewer.ShowOrderedArticles();
            ConsoleViewer.ShowUnpublishedAuthors();


            
        }
    }
}
