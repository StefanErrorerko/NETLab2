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
            var data = new Data();
            var writeXml = new WriterXml();
            var consoleViewer = new ConsoleViewer();
            Console.OutputEncoding = Encoding.UTF8;

            consoleViewer.data = data;

            consoleViewer.DisplayCreateAuthor();
            consoleViewer.DisplayCreateMagazine();
            consoleViewer.DisplayCreateArticle();
            consoleViewer.DisplayCreateDoc();
            consoleViewer.DisplayAllCreated();

            writeXml.CreateXml(data);
            ReaderXml readXml = new ReaderXml();
            var queries = new Queries(readXml);
            consoleViewer.QueriesContainer = queries;

            consoleViewer.ShowArticlesUnpublished();
            consoleViewer.ShowMagsEtEstbl();
            consoleViewer.ShowMagsWithNormalCirc();
            consoleViewer.ShowArticlesU2014();
            consoleViewer.ShowMagsFreqU2();
            consoleViewer.ShowMagFirstBeforeIndependence();
            consoleViewer.ShowMagsAndItsArticles();
            consoleViewer.ShowAuthorsAndItsArticles();
            consoleViewer.ShowCircSummary();
            consoleViewer.ShowArticlesGroupByPublish();
            consoleViewer.ShowArticlesGroupByYearOver2002();
            consoleViewer.ShowArticlesInPotopMag();
            consoleViewer.ShowAuthorsExceptedWriterOfUkraina();
            consoleViewer.ShowFirstAndLastDoc();
            consoleViewer.ShowAuthorsInPotopAndTerra();
        }
    }
}
