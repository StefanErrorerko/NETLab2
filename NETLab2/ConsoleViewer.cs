using System;
using System.Linq;
using System.Xml.Linq;
using NET_Lab2.Instruments;
using NET_Lab2.Entities;
using System.Threading;
using System.Collections.Generic;

namespace NET_Lab2
{
    public class ConsoleViewer
    {
        private int[] _count = new int[3];
        public Data data { get; set; }
        public Queries QueriesContainer { get; set; }

        //1
        public void ShowArticlesUnpublished()
        {
            ShowTitle(1, "Статті, що не були опубліковані");
            foreach(var article in QueriesContainer.GetArticlesUnpublished())
            {
                Console.WriteLine($"{article.Name} - {data.Authors[article.AuthorId-1]}");
            };
            Console.WriteLine('\n');
        }
        //2
        public void ShowMagsEtEstbl()
        {
            ShowTitle(2, "вибiрка журналів та років їх заснування");
            foreach (var info in QueriesContainer.GetMagsNameEtEstbl())
            {
                Console.WriteLine(info);
            }
            Console.WriteLine('\n');

        }
        //3
        public void ShowMagsWithNormalCirc()
        { 
            ShowTitle(3, "Журнали з малим тиражем(<5000)");
            foreach(var mag in QueriesContainer.GetMagsWithLowCirc())
            {
                Console.WriteLine(mag);
            };
            Console.WriteLine('\n');

        }
        //4
        public void ShowArticlesU2014()
    {
            ShowTitle(4, "вибрати статті, що були опубліковані до 2014 року");
            foreach (var article in QueriesContainer.GetArticlesU2014())
            {
                Console.WriteLine($"{article.Name} - {data.Authors[article.AuthorId-1]}");
            }
            Console.WriteLine('\n');
        }
        //5
        public void ShowMagsFreqU2()
    {
            ShowTitle(5, "вибрати журнали з малою періодичністю ( менше 2 на місяць)");
            foreach (var dict in QueriesContainer.GetMagsFreqU2())
            {
                Console.WriteLine(dict.Key + ", Frequency: " + dict.Value);
            }
            Console.WriteLine('\n');
        }

        //6
        public void ShowMagFirstBeforeIndependence()
    {
            ShowTitle(6, "вибрати перший журнал, що був заснований " +
                "до проголошення незалежностi");
            Console.WriteLine(QueriesContainer.GetMagFirstBeforeIndependence());
            Console.WriteLine('\n');
        }

        //7
        public void ShowMagsAndItsArticles()
    {
            ShowTitle(7, "вибрати журнал та опубліковані в ньому статті");
            foreach (var group in QueriesContainer.GetMagsAndArticles())
            {
                Console.Write($"Articles in mag {group.Key.Name} - ");
                foreach (var articles in group)
                {
                    foreach(var article in articles)
                    {
                        Console.WriteLine(article.Name);
                    }
                        
                }
            }
            Console.WriteLine('\n');
        }

        //8 
        public void ShowAuthorsAndItsArticles()
    {
            ShowTitle(8, "вибрати автори та статтi, якi тi друкувались");
            foreach (var group in QueriesContainer.GetAuthorsAndItsArticles())
            {
                Console.WriteLine(group.Key);
                Console.WriteLine($"\t{group}");
            }
            Console.WriteLine('\n');
        }

        //9
        public void ShowCircSummary()
    {

            ShowTitle(9, " оцiнити сумарний наклад усiх журналiв на рiк");
            foreach (var mag in QueriesContainer.GetMagsAndCirc())
            {
                Console.WriteLine($"Mag: {mag.Key} - {mag.Value}pcs/yr");
            }
            Console.WriteLine($"Sum per yr: {QueriesContainer.GetCircSummary()}");
            Console.WriteLine('\n');
        }

        //10
        public void ShowArticlesGroupByPublish()
    {
            ShowTitle(10, "згрупувати усi статтi за кiлькiстю їх публiкацiй");
            var q = QueriesContainer.GetArticlesGroupByPublish();
            foreach (var group in q)
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
        public void ShowArticlesGroupByYearOver2002()
    {
            ShowTitle(11, "згрупувати усi статтi пiсля 2002 за роком публiкацiї (Group Any)");
            var q = QueriesContainer.GetArticlesGroupByYearOver2002();
            foreach (var group in q)
            {
                Console.WriteLine($"Key: {group.Key}");
                foreach (var doc in group.Value)
                    Console.WriteLine(doc);
            }
            Console.WriteLine('\n');
        }

        //12
        public void ShowArticlesInPotopMag()
    {
            ShowTitle(12, "Вивести iнформацiю про статтi, що публiкувались у 'Potop' Mag");
            foreach(var article in QueriesContainer.GetArticlesInPotopMag())
            {
                Console.WriteLine(article);
            };
            Console.WriteLine('\n');
        }

    //13
    public void ShowAuthorsExceptedWriterOfUkraina()
    {
        ShowTitle(13, "Вивести усіх авторів, окрім автора статті \"Ukarina\"");
        foreach(var author in QueriesContainer.GetAuthorsExceptedWriterOfUkraina())
        {
            Console.WriteLine(author);
        };
        Console.WriteLine('\n');
    }

    //14
    public void ShowFirstAndLastDoc()
    {
        ShowTitle(14, "Вивести документацію про першу та останню опубліковану статтю");
        foreach(var doc in QueriesContainer.GetFirstAndLastDoc())
        {
                Console.WriteLine($"{doc.Date.ToString("d")}': " +
                    $"Released '{data.Articles[doc.ArticleId-1].Name}' " +
                    $"in mag '{data.Mags[doc.MagId-1].Name}'");
        }
        Console.WriteLine('\n');
    }


    //15
    public void ShowAuthorsInPotopAndTerra()
    {
        ShowTitle(15, "Автори, що публікувались у Potop i Terra");
        foreach(var author in QueriesContainer.GetAuthorsInPotopAndTerra())
        {
            Console.WriteLine(author);
        };
        Console.WriteLine('\n');
    }

        private void ShowTitle(int num, string title)
        {
            Console.WriteLine($"=======запит {num} - {title}=======");
        }

        public void DisplayCreateAuthor()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Скільки авторів створимо?");
            Console.ResetColor();
            ReadCount(0);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Аби створити перелік авторів, уведіть про них інформацію у форматі: Прізвище, Ім'я, По-батькові, Спілка");
            Console.ResetColor();
            for (int i = 0; i < _count[0]; i++)
            {
                try
                {
                    var authorData = Console.ReadLine();
                    data.Authors.Add(CreateAuthor(authorData));
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ви невірно ввели дані! Спробуйте знов");
                    --i;
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Дані вписані!");
        }

        public void DisplayCreateMagazine()
        {
            // Запис журналів
            Console.WriteLine("Скільки створимо журналів?");
            Console.ResetColor();
            ReadCount(1);
            // Тираж примірників на місяць
            // Періодичність виходу одного примірника на місяць
            // Друге - дата заснування журналу
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Аби створити перелік журналів, уведіть про них інформацію у форматі: Назва, 01/01/2020, Тираж, Періодичність");
            Console.ResetColor();
            for (int i = 0; i < _count[1]; i++)
            {
                try
                {
                    string magData = Console.ReadLine();
                    int id = data.Mags.Count() + 1;
                    data.Mags.Add(CreateMag(magData));
                }
                catch(ImpossibleDateException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний формат дати");
                    --i;
                    Console.ResetColor();
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ви невірно ввели дані! Спробуйте знов");
                    --i;
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Дані вписані!");
        }

        public void DisplayCreateArticle()
        {
            // Запис статей
            Console.WriteLine("Скільки створимо статей?");
            Console.ResetColor();
            ReadCount(2);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Аби створити перелік статей, уведіть про них інформацію у форматі: Назва, 'Номер автора у списку'");

            Console.ResetColor();
            for (int i = 0; i < _count[2]; i++)
            {
                try
                {
                    string stringData = Console.ReadLine();
                    data.Articles.Add(CreateArticle(stringData));
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ви невірно ввели дані! Спробуйте знов");
                    --i;
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Дані вписані!");
        }

        public void DisplayCreateDoc()
        {
            try
            {
                //Опубліковування статей
                Console.Write("Очікуємо публікування статей.");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.WriteLine(".");
                CreateDocs();
            }
            catch(ImpossibleDateException ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }

        public void DisplayAllCreated()
        {
            Console.WriteLine("Маємо такі колекції:");
            Console.ResetColor();

            Console.WriteLine("Автори:");
            foreach (var author in data.Authors)
            {
                Console.WriteLine(author);
            }

            Console.WriteLine("Журнали:");
            foreach (var mag in data.Mags)
            {
                Console.WriteLine(mag);
            }

            Console.WriteLine("Статті:");
            foreach (var article in data.Articles)
            {
                Console.WriteLine($"{article.Name} - {data.Authors[article.AuthorId-1]}");
            }

            Console.WriteLine("Опубліковане:");
            foreach (var doc in data.Docs)
            {
                Console.WriteLine($"{doc.Date.ToString("d")}: " +
                    $"#{doc.ArticleId} {data.Articles[doc.ArticleId-1]} " +
                    $"in #{doc.MagId} {data.Mags[doc.MagId-1]}");
            }
        }

        private void ReadCount(int i)
        {
            bool isInputCorrect;
            do
            {
                isInputCorrect = Int32.TryParse(Console.ReadLine(), out int x);
                Console.ForegroundColor = ConsoleColor.Red;
                if (isInputCorrect)
                {
                    _count[i] = x;
                }
                else Console.WriteLine("Невірні значення. Спробуйте знов");
                Console.ResetColor();
            } while (!isInputCorrect);
        }

        private Author CreateAuthor(string authorData)
        {
            string[] stingData = authorData.Split(',');
            int id = data.Authors.Count() + 1;
            var author = new Author() 
            { 
                AuthorId = id, 
                Surname = stingData[0], 
                Name = stingData[1], 
                Secondname = stingData[2], 
                Organ = stingData[3] 
            };
            return author;
        }
        private Magazine CreateMag(string magData)
        {
            string[] stingData = magData.Split(',');
            int id = data.Mags.Count() + 1;
            string name = stingData[0];
            var est = Convert.ToDateTime(stingData[1]);
            DateTimeAccuracyMonitor.CheckDate(est);
            int circ = Int32.Parse(stingData[2]);
            double freq = Convert.ToDouble(stingData[3]);
            var mag = new Magazine() 
            { 
                MagId = id, 
                Name = name, 
                Est = est, 
                Circ = circ, 
                Freq = freq 
            };
            return mag;
        }
        private Article CreateArticle(string articleData)
        {
            string[] stingData = articleData.Split(',');
            string name = stingData[0];
            int authorid = Int32.Parse(stingData[1]);
            if (authorid < 1 || authorid > data.Authors.Count())
            {
                throw new UnexpectedIdException("Id gets out of boundaries");
            }
            int id = data.Articles.Count() + 1;
            var article = new Article() 
            { 
                ArticleId = id, 
                Name = name, 
                AuthorId = authorid 
            };
            return article;
        }
        private void CreateDocs()
        {
            var rnd = new Random();
            DateTime date;
            Article article;
            Magazine mag;
            // кількість опублікування однієї статті
            int publishCount;

            for (int i = 0; i < _count[2]; i++)
            {
                article = data.Articles[i];
                publishCount = rnd.Next(0, 4);
                for (int j = 0; j < publishCount; j++)
                {                       
                    // випадковий журнал
                    mag = data.Mags[rnd.Next(0, data.Mags.Count)];
                    
                    // випадкова дата
                    var start = mag.Est;
                    var range = (DateTime.Now - start).Days;
                    date = start.AddDays(rnd.Next(range));

                    DateTimeAccuracyMonitor.CheckDate(date);

                    int id = data.Docs.Count() + 1;
                    var doc = new EditorDoc() 
                    { 
                        DocId = id, 
                        Date = date, 
                        ArticleId = article.ArticleId, 
                        MagId = mag.MagId 
                    };
                    data.Docs.Add(doc);
                }
            }
        }
    }
}
