using System;
using System.Text;
using NET_Lab2.XmlProcessors;
using NET_Lab2.DataManagers;
using NET_Lab2.QueryContainers;
using NET_Lab2.Output;

namespace NET_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Data();
            var writeXml = new WriterXml();
            var consoleViewer = new ConsoleViewer(data);
            Console.OutputEncoding = Encoding.UTF8;

            consoleViewer.DisplayCreateAuthor();
            consoleViewer.DisplayCreateMagazine();
            consoleViewer.DisplayCreateArticle();
            consoleViewer.DisplayCreateDoc();

            consoleViewer.DisplayAll();

            writeXml.CreateXml(data);

            var readXml = new ReaderXml();
            var queries = new Queries(readXml);
            consoleViewer.QueriesContainer = queries;

            var beginOfWarOnDonbas = new DateTime(2014, 4, 12);
            var dayOfIndependence = new DateTime(1991, 8, 24);
            var articleName1 = "Ukraina";
            var magName1 = "Potop";
            var magName2 = "Terra";
            var circ = 5000;

            consoleViewer.ShowArticlesUnpublished();
            consoleViewer.ShowMagsAndEstbl();
            consoleViewer.ShowMagsWithCirc(circ);
            consoleViewer.ShowArticlesInBounds(beginOfWarOnDonbas);
            consoleViewer.ShowMagsFreqInBounds(2);
            consoleViewer.ShowMagFirstInBounds(dayOfIndependence);
            consoleViewer.ShowMagsAndItsArticles();
            consoleViewer.ShowAuthorsAndItsArticles();
            consoleViewer.ShowCircSummary();
            consoleViewer.ShowArticlesGroupByPublish();
            consoleViewer.ShowArticlesGroupByYear();
            consoleViewer.ShowArticlesInMag(magName1);
            consoleViewer.ShowAuthorsExceptedWriterOfArticle(articleName1);
            consoleViewer.ShowFirstAndLastDoc();
            consoleViewer.ShowAuthorsInTwoMags(magName1, magName2);
        }
    }
}
