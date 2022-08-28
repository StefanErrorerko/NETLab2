using System;
using System.Linq;
using System.Xml.Linq;
using NET_Lab2.Instruments;
using NET_Lab2.Entities;
using System.Threading;
using System.Collections.Generic;

namespace NET_Lab2
{
    internal static class ConsoleViewer
    {
        private static int[] _count = new int[3];
        internal static Data data { get; set; }

        //1 переп
        public static void ShowMags()
        {
            ShowTitle(1, "вибiрка всiх журналів");
            DefaultShow(Queries.GetMags());
            Console.WriteLine('\n');
        }
        //2
        public static void ShowMagsEtEstbl()
        {
            ShowTitle(2, "вибiрка журналів та років їх заснування");
            foreach (var info in Queries.GetMagsNameEtEstbl())
            {
                Console.WriteLine(info);
            }
            Console.WriteLine('\n');

        }
        //3
        public static void ShowMagsWithNormalCirc()
        { 
            ShowTitle(3, "Журнали з малим тиражем(<5000)");
            DefaultShow(Queries.GetMagsWithLowCirc());
            Console.WriteLine('\n');

        }
        //4
        public static void ShowArticlesU2014()
    {
            ShowTitle(4, "вибрати статті, що були опубліковані до 2014 року");
            foreach (var lkp in Queries.GetArticlesU2014())
            {
                Console.WriteLine(lkp.Key);
                foreach(var smth in lkp) Console.WriteLine(smth);
            }
            Console.WriteLine('\n');
        }
        //5
        public static void ShowMagsFreqU2()
    {
            ShowTitle(5, "вибрати журнали з малою періодичністю ( менше 2 на місяць)");
            foreach (var dict in Queries.GetMagsFreqU2())
            {
                Console.WriteLine(dict.Key + " " + dict.Value);
            }
            Console.WriteLine('\n');
        }

        //6
        public static void ShowFirstMagOfIndependentUA()
    {
            ShowTitle(6, "вибрати перший журнал, що був заснований " +
                "до проголошення незалежностi");
            DefaultShow(Queries.GetMagFirstOfIndependentUA());
            Console.WriteLine('\n');
        }

        //7
        public static void ShowMagsAndItsArticles()
    {
            ShowTitle(7, "вибрати журнал та опубліковані в ньому статті" +
                "у ньому (Group Join)");
            foreach (var group in Queries.GetMagsAndArticles())
            {
                Console.Write($"Articles in mag {group.Key} - ");
                foreach (var articles in group)
                {
                    foreach(var article in articles)
                    {
                        Console.WriteLine(article);
                    }
                        
                }
            }
            Console.WriteLine('\n');
        }

        //8 n???
        public static void ShowAuthorsAndItsArticles()
    {
            ShowTitle(8, "вибрати автори та статтi, якi тi друкувались " +
                "(Outer Join)");
            foreach (var group in Queries.GetAuthorsAndItsArticles())
            {
                Console.WriteLine(group);
            }
            Console.WriteLine('\n');
        }

        //9
        public static void ShowCircSummary()
    {

            ShowTitle(9, " оцiнити сумарний наклад усiх журналiв на рiк");
            foreach (var mag in Queries.GetMagsAndCirc())
            {
                Console.WriteLine($"Mag: {mag.Key} - {mag.Value}pcs/yr");
            }
            Console.WriteLine($"Sum per yr: {Queries.GetCircSummary()}");
            Console.WriteLine('\n');
        }

        //10
        public static void ShowArticlesGroupByPublish()
    {
            ShowTitle(10, "згрупувати усi статтi за кiлькiстю їх публiкацiй");
            var q = Queries.GetArticlesGroupByPublish();
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
        public static void ShowArticlesGroupByYearOver2002()
    {
            ShowTitle(11, "згрупувати усi статтi пiсля 2002 за роком публiкацiї (Group Any)");
            var q = Queries.GetArticlesGroupByYearOver2002();
            foreach (var group in q)
            {
                Console.WriteLine($"Key: {group.Key}");
                foreach (var doc in group.Value)
                    Console.WriteLine(doc);
            }
            Console.WriteLine('\n');
        }

        //12
        public static void ShowArticlesInPotopMag()
    {
            ShowTitle(12, "Вивести iнформацiю про статтi, що публiкувались у 'Potop' Mag");
            DefaultShow(Queries.GetArticlesInPotopMag());
            Console.WriteLine('\n');
        }

    //13
    public static void ShowConcatedLists()
    {
        ShowTitle(13, "об'єднати перший та останній член масиву");
        DefaultShow(Queries.GetConcatedLists());
        Console.WriteLine('\n');
    }

    //14
    public static void ShowOrderedArticles()
    {
        ShowTitle(14, "відсортовані по назвах статті");
        DefaultShow(Queries.GetOrderedArticles());
        Console.WriteLine('\n');
    }


    //15
    public static void ShowUnpublishedAuthors()
    {
        ShowTitle(14, "Автори, що не публікувались");
        DefaultShow(Queries.GetUnpublichedAuthors());
        Console.WriteLine('\n');
    }

    //==============================================
    private static void DefaultShow(IEnumerable<object> q)
        {
            foreach (var obj in q)
            {
                Console.WriteLine(obj);
            }
        }

        private static void ShowTitle(int num, string title)
        {
            Console.WriteLine($"=======запит {num} - {title}=======");
        }

        // Запис авторів

        public static void DisplayCreateAuthor()
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

        public static void DisplayCreateMagazine()
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

        public static void DisplayCreateArticle()
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

        public static void DisplayCreateDoc()
        {
            //Опубліковування статей
            Console.Write("Очікуємо публікування статей.");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.WriteLine(".");
            CreateDocs();
        }

        public static void DisplayAllCreated()
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
                Console.WriteLine($"{article.Name} - {data.Authors[article.AuthorId]}");
            }

            Console.WriteLine("Опубліковане:");
            foreach (var doc in data.Docs)
            {
                Console.WriteLine($"{doc.Date.ToString("d")}: " +
                    $"#{doc.ArticleId} {data.Articles[doc.ArticleId-1]} " +
                    $"in #{doc.MagId} {data.Mags[doc.MagId-1]}");
            }
        }

        private static void ReadCount(int i)
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

        private static Author CreateAuthor(string authorData)
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
        private static Magazine CreateMag(string magData)
        {
            string[] stingData = magData.Split(',');
            int id = data.Mags.Count() + 1;
            string name = stingData[0];
            var est = Convert.ToDateTime(stingData[1]);
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
        private static Article CreateArticle(string articleData)
        {
            string[] stingData = articleData.Split(',');
            string name = stingData[0];
            int authorid = Int32.Parse(stingData[1]) - 1;
            if (authorid < 0 || authorid > data.Authors.Count() - 1)
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
        private static void CreateDocs()
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
                publishCount = rnd.Next(0, 3);
                for (int j = 0; j < publishCount; j++)
                {
                    // випадкова дата
                    date = new DateTime(rnd.Next(1991, 2022), rnd.Next(1, 12), rnd.Next(1, 30));
                    // випадковий журнал
                    mag = data.Mags[rnd.Next(0, data.Mags.Count - 1)];
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
