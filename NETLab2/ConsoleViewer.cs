using System;
using System.Linq;
using System.Xml.Linq;
using NET_Lab2.Instruments;
using NET_Lab2.Entity;
using System.Threading;
using System.Collections.Generic;

namespace NET_Lab2
{
    internal static class ConsoleViewer
    {
        private static int[] count = new int[3];
        internal static Data data { get; set; }

        //1
        public static void ShowMags()
        {
            ShowTitle(1, "вибiрка всiх журналів");
            DefaultShow(Queries.GetMags());
            Console.WriteLine('\n');
        }
        //2
        public static void ShowMagsEtEstbl()
        {
            ShowTitle(2, "вибiрка авторів, відсортованих за прізвищем");
            foreach (var x in Queries.GetAuthorsBySurname()) Console.WriteLine(x);
            Console.WriteLine('\n');

        }
        //3
        public static void ShowMagsWithNormalCirc()
        { 
            ShowTitle(3, "Журнали з середнiм тиражем(10000<= && <=20000)");
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
        public static void ShowMagsFreqU1()
    {
            ShowTitle(5, "вибрати журнали з малою періодичністю ( менше 1 на місяць)");
            foreach (var dict in Queries.GetMagsFreqU1())
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
            ShowTitle(7, "вибрати журнал та авторiв, що друкувались " +
                "у ньому (Group Join)");
            foreach (var x in Queries.GetMagsAndItsArticles())
            {
                Console.Write($"Articles in mag {x.Key} - ");
                foreach (var y in x) Console.Write(y);
            }
            Console.WriteLine('\n');
        }

        //8
        public static void ShowAuthorsAndItsArticles()
    {
            ShowTitle(8, "вибрати автори та статтi, якi тi друкувались " +
                "(Outer Join)");
            foreach (var x in Queries.GetAuthorsAndItsArticles())
            {
                Console.WriteLine(x.Key);
                foreach (var y in x) Console.WriteLine($"\t{y.Key}");
            }
            Console.WriteLine('\n');
        }

        //9
        public static void ShowCircSummary()
    {

            ShowTitle(9, " оцiнити сумарний наклад усiх журналiв на рiк");
            foreach (var x in Queries.GetMagsAndCirc()) Console.WriteLine($"Mag: {x.Key} - {x.Value}pcs/yr");
            Console.WriteLine($"Sum per yr: {Queries.GetCircSummary()}");
            Console.WriteLine('\n');
        }

        //10
        public static void ShowArticlesGroupByPublish()
    {
            ShowTitle(10, "згрупувати усi статтi за кiлькiстю їх публiкацiй");
        var q = Queries.GetArticlesGroupByPublish();
            foreach (var x in q)
            {
                Console.WriteLine(x.Key);
                foreach (var y in x)
                {
                    Console.WriteLine(y);
                }
            }
            Console.WriteLine('\n');
        }

        //11
        public static void ShowArticlesGroupByYearOver2002()
    {
            ShowTitle(11, "згрупувати усi статтi пiсля 2002 за роком публiкацiї (Group Any)");
            var q = Queries.GetArticlesGroupByYearOver2002();
            foreach (var x in q)
            {
                Console.WriteLine($"Key: {x.Key}");
                foreach (var y in x.Value)
                    Console.WriteLine((string)y);
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
            foreach (var x in q) Console.WriteLine(x);
        }

        private static void DefaultShow(object obj)
        {
            Console.WriteLine(obj);
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
            for (int i = 0; i < count[0]; i++)
            {
                try
                {
                    string authordata = Console.ReadLine();
                    data.Authors.Add(CreateAuthor(authordata));
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
            for (int i = 0; i < count[1]; i++)
            {
                try
                {
                    string magdata = Console.ReadLine();
                    int id = data.Mags.Count() + 1;
                    data.Mags.Add(CreateMag(magdata));
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
            for (int i = 0; i < count[2]; i++)
            {
                try
                {
                    string stringdata = Console.ReadLine();
                    data.Articles.Add(CreateArticle(stringdata));
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
            foreach (Author x in data.Authors) Console.WriteLine(x);
            Console.WriteLine("Журнали:");
            foreach (Magazine x in data.Mags) Console.WriteLine(x);
            Console.WriteLine("Статті:");
            foreach (Article x in data.Articles) Console.WriteLine(x);
            Console.WriteLine("Опубліковане:");
            foreach (EditorDoc x in data.Docs) Console.WriteLine(x);
        }

        private static void ReadCount(int i)
        {
            bool flag;
            do
            {
                flag = Int32.TryParse(Console.ReadLine(), out int x);
                Console.ForegroundColor = ConsoleColor.Red;
                if (flag) count[i] = x;
                else Console.WriteLine("Невірні значення. Спробуйте знов");
                Console.ResetColor();
            } while (!flag);
        }

        private static Author CreateAuthor(string authordata)
        {
            string[] strdata = authordata.Split(',');
            int id = data.Authors.Count() + 1;
            Author author = new Author() { AuthorId = id, Surname = strdata[0], Name = strdata[1], Secondname = strdata[2], Organ = strdata[3] };
            return author;
        }
        private static Magazine CreateMag(string magdata)
        {
            string[] strdata = magdata.Split(',');
            int id = data.Mags.Count() + 1;
            string name = strdata[0];
            DateTime est = Convert.ToDateTime(strdata[1]);
            int circ = Int32.Parse(strdata[2]);
            double freq = Convert.ToDouble(strdata[3]);
            Magazine mag = new Magazine() { MagId = id, Name = name, Est = est, Circ = circ, Freq = freq };
            return mag;
        }
        private static Article CreateArticle(string artdata)
        {
            string[] stringdata = artdata.Split(',');
            string name = stringdata[0];
            int authorid = Int32.Parse(stringdata[1]) - 1;
            if (authorid < 0 || authorid > data.Authors.Count() - 1) throw new UnexpectedIdException("Id gets out of boundaries");


            int id = data.Articles.Count() + 1;
            Article article = new Article() { ArticleId = id, Name = name, AuthorId = authorid };
            return article;
        }
        private static void CreateDocs()
        {
            Random rnd = new Random();
            DateTime date;
            Article ar;
            Magazine mag;
            // кількість опублікування однієї статті
            int publishcount;


            for (int i = 0; i < count[2]; i++)
            {
                ar = data.Articles[i];
                publishcount = rnd.Next(0, 3);
                for (int j = 0; j < publishcount; j++)
                {
                    // випадкова дата
                    date = new DateTime(rnd.Next(1991, 2022), rnd.Next(1, 12), rnd.Next(1, 30));
                    // випадковий журнал
                    mag = data.Mags[rnd.Next(0, data.Mags.Count - 1)];
                    int id = data.Docs.Count() + 1;
                    EditorDoc doc = new EditorDoc() { DocId = id, Date = date, ArticleId = ar.ArticleId, MagId = mag.MagId };

                    data.Docs.Add(doc);
                }
            }

        }
    }
}
