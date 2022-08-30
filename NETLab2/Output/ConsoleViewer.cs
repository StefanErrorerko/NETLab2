using System;
using System.Linq;
using NET_Lab2.Instruments;
using NET_Lab2.DataManagers;
using NET_Lab2.QueryContainers;

namespace NET_Lab2.Output
{
    public class ConsoleViewer
    {
        private int[] _count = new int[3];
        private Data _data;

        public Queries QueriesContainer { get; set; }

        public ConsoleViewer(Data data)
        {
            _data = data;
        }

        //1
        public void ShowArticlesUnpublished()
        {
            ShowTitle(1, "unpublished articles");
            foreach (var article in QueriesContainer.GetArticlesUnpublished())
            {
                Console.WriteLine($"{article.Name} - {_data.Authors[article.AuthorId - 1]}");
            };
            Console.WriteLine('\n');
        }
        //2
        public void ShowMagsAndEstbl()
        {
            ShowTitle(2, "magazines and it establishment date");
            foreach (var info in QueriesContainer.GetMagsNameAndEstbl())
            {
                Console.WriteLine(info);
            }
            Console.WriteLine('\n');

        }
        //3
        public void ShowMagsWithCirc(int circ)
        {
            ShowTitle(3, $"magazines with circulation under {circ} ");
            foreach (var mag in QueriesContainer.GetMagsWithCirc(circ))
            {
                Console.WriteLine(mag);
            };
            Console.WriteLine('\n');
        }
        //4
        public void ShowArticlesInBounds(DateTime highbound)
        {
            ShowTitle(4, $"articles, published till {highbound.ToString("d")}");
            foreach (var article in QueriesContainer.GetArticlesInBounds(highbound))
            {
                Console.WriteLine($"{article.Name} - {_data.Authors[article.AuthorId - 1]}");
            }
            Console.WriteLine('\n');
        }
        //5
        public void ShowMagsFreqInBounds(double highbound, double lowbound = 0)
        {
            ShowTitle(5, $"magazines with circulation from {lowbound} till {highbound} per month)");
            foreach (var dict in QueriesContainer.GetMagsFreqInBounds(lowbound, highbound))
            {
                Console.WriteLine(dict.Key + ", Frequency: " + dict.Value);
            }
            Console.WriteLine('\n');
        }

        //6
        public void ShowMagFirstInBounds(DateTime highbound)
        {
            ShowTitle(6, $"first magazine that was established till {highbound.ToString("d")}");
            Console.WriteLine(QueriesContainer.GetMagFirstInBounds(highbound));
            Console.WriteLine('\n');
        }

        //7
        public void ShowMagsAndItsArticles()
        {
            ShowTitle(7, "choose each mag and articles published into it");
            foreach (var group in QueriesContainer.GetMagsAndArticles())
            {
                Console.Write($"Articles in mag {group.Key.Name}:\n");
                foreach (var articles in group)
                {
                    foreach (var article in articles)
                    {
                        Console.WriteLine($"\t{article.Name}");
                    }

                }
            }
            Console.WriteLine('\n');
        }

        //8 
        public void ShowAuthorsAndItsArticles()
        {
            ShowTitle(8, "choose each author and articles they wrote");
            foreach (var group in QueriesContainer.GetAuthorsAndItsArticles())
            {
                Console.WriteLine(group.Key);
                foreach (var article in group)
                {
                    Console.WriteLine($"\t{article}");
                }
            }
            Console.WriteLine('\n');
        }

        //9
        public void ShowCircSummary()
        {

            ShowTitle(9, "estimate a total circulation of magazines in one year");
            foreach (var mag in QueriesContainer.GetMagsWithYearCirc())
            {
                Console.WriteLine($"Mag: {mag.Key} - {mag.Value}pcs/yr");
            }
            Console.WriteLine($"Sum per yr: {QueriesContainer.GetCircSummary()}");
            Console.WriteLine('\n');
        }

        //10
        public void ShowArticlesGroupByPublish()
        {
            ShowTitle(10, "group all articles by an amount of its publication");
            var query = QueriesContainer.GetArticlesGroupByPublish();
            foreach (var group in query)
            {
                Console.WriteLine(group.Key);
                foreach (var article in group)
                {
                    Console.WriteLine(article);
                }
            }
            Console.WriteLine('\n');
        }

        //11
        public void ShowArticlesGroupByYear()
        {
            ShowTitle(11, "group all articles by year of its publication");
            var query = QueriesContainer.GetArticlesGroupByYear();
            foreach (var group in query)
            {
                Console.WriteLine($"Key: {group.Key}");
                foreach (var doc in group.Value)
                    Console.WriteLine($"'{_data.Articles[doc.ArticleId - 1]}' in '{_data.Mags[doc.MagId - 1]}' mag");
            }
            Console.WriteLine('\n');
        }

        //12
        public void ShowArticlesInMag(string magName)
        {
            ShowTitle(12, $"info about articles published in {magName}");
            foreach (var article in QueriesContainer.GetArticlesInMag(magName))
            {
                Console.WriteLine($"{article.Name} - {_data.Authors[article.AuthorId - 1]}");
            };
            Console.WriteLine('\n');
        }

        //13
        public void ShowAuthorsExceptedWriterOfArticle(string articleName)
        {
            ShowTitle(13, $"output all authors except an author of '{articleName}'");
            foreach (var author in QueriesContainer.GetAuthorsExceptedWriterOfArticle(articleName))
            {
                Console.WriteLine(author);
            };
            Console.WriteLine('\n');
        }

        //14
        public void ShowFirstAndLastDoc()
        {
            ShowTitle(14, "output documents about the first and the last published articles");
            foreach (var doc in QueriesContainer.GetFirstAndLastDoc())
            {
                Console.WriteLine($"{doc.Date.ToString("d")}': " +
                    $"Released '{_data.Articles[doc.ArticleId - 1].Name}' " +
                    $"in mag '{_data.Mags[doc.MagId - 1].Name}'");
            }
            Console.WriteLine('\n');
        }


        //15
        public void ShowAuthorsInTwoMags(string magName1, string magName2)
        {
            ShowTitle(15, $"authors, that were published in {magName1} and {magName2}");
            foreach (var author in QueriesContainer.GetAuthorsInTwoMags(magName1, magName2))
            {
                Console.WriteLine(author);
            };
            Console.WriteLine('\n');
        }

        private void ShowTitle(int num, string title)
        {
            Console.WriteLine($"=======query {num} - {title}=======");
        }

        public void DisplayCreateAuthor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("How many authors would you like to input?");
            Console.ResetColor();
            ReadCount(0);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("In order to create a list of authors, input info about them in a format: " +
                "Surname, Name, Secondname, Organisation");
            Console.ResetColor();

            var creator = new DataCreator();
            for (int i = 0; i < _count[0]; i++)
            {
                try
                {
                    var authorData = Console.ReadLine();
                    var authorCount = _data.Authors.Count();
                    _data.Authors.Add(creator.CreateAuthor(authorData, authorCount));
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have incorrectly input values! Try again!");
                    --i;
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Values are taken!");
        }

        public void DisplayCreateMagazine()
        {
            Console.WriteLine("How many magazines would you like to input?");
            Console.ResetColor();
            ReadCount(1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("In order to create a list of magazines, input info about them in a format: " + 
                "Name, Established (in a format: 01/01/2020), Circulation, Frequency");
            Console.ResetColor();

            var creator = new DataCreator();
            for (int i = 0; i < _count[1]; i++)
            {
                try
                {
                    var magData = Console.ReadLine();
                    var magCount = _data.Mags.Count();
                    var id = magCount + 1;
                    _data.Mags.Add(creator.CreateMag(magData, magCount));
                }
                catch (ImpossibleDateException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid data input!");
                    --i;
                    Console.ResetColor();
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have incorrectly input values! Try again!");
                    --i;
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Values are taken!");
        }

        public void DisplayCreateArticle()
        {
            Console.WriteLine("How many articles would you like to input?");
            Console.ResetColor();
            ReadCount(2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("In order to create a list of magazines, input info about them in a format: " +
                "Name, Index of the author in a list as integer");

            var creator = new DataCreator();
            Console.ResetColor();
            for (int i = 0; i < _count[2]; i++)
            {
                try
                {
                    var stringData = Console.ReadLine();
                    var articleCount = _data.Articles.Count;
                    var article = creator.CreateArticle(stringData, articleCount);
                    
                    if(article is null)
                    {
                        throw new UnexpectedIdException("Index got out of boundaries");
                    }

                    _data.Articles.Add(article);
                }
                catch (UnexpectedIdException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    --i;
                    Console.ResetColor();
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have incorrectly input values! Try again!");
                    --i;
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Values are taken!");
        }

        public void DisplayCreateDoc()
        {
            try
            {
                Console.Write("Publishing articles... Creating documents...");
                var creator = new DataCreator();
                _data.Docs = creator.GenerateDocs(_data);
            }
            catch (ImpossibleDateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayAll()
        {
            Console.WriteLine("Here are such lists as:");
            Console.ResetColor();

            Console.WriteLine("Authors:");
            foreach (var author in _data.Authors)
            {
                Console.WriteLine(author);
            }

            Console.WriteLine("Magazines:");
            foreach (var mag in _data.Mags)
            {
                Console.WriteLine(mag);
            }

            Console.WriteLine("Articles:");
            foreach (var article in _data.Articles)
            {
                Console.WriteLine($"{article.Name} - {_data.Authors[article.AuthorId - 1]}");
            }

            Console.WriteLine("Published:");
            foreach (var doc in _data.Docs)
            {
                Console.WriteLine($"{doc.Date.ToString("d")}: " +
                    $"#{doc.ArticleId} {_data.Articles[doc.ArticleId - 1]} " +
                    $"in #{doc.MagId} {_data.Mags[doc.MagId - 1]}");
            }
        }

        // read an input value while it is incorrect. then record a count of data objects
        // int index - an index of _count
        // _count[0] - a count of created authors
        // _count[1] - a count of created mags
        // _count[] - a count of created articles
        private void ReadCount(int index)
        {
            bool isInputCorrect;
            do
            {
                isInputCorrect = int.TryParse(Console.ReadLine(), out var count);
                Console.ForegroundColor = ConsoleColor.Red;
                if (isInputCorrect)
                {
                    _count[index] = count;
                }
                else
                {
                    Console.WriteLine("You have incorrectly input values! Try again!");
                }
                Console.ResetColor();
            } while (!isInputCorrect);
        }
    }
}
