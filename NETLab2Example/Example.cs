using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NETLab2
{
    class Example
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Створюємо файл
            IList<User> users = new List<User> {
                new User ("Bill Gates", "Microsoft", 48),
                new User ("Larry Page", "Google", 42)
             };
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create("users.xml", settings))
            {
                writer.WriteStartElement("users");
                foreach (User user in users)
                {
                    writer.WriteStartElement("user");
                    writer.WriteElementString("name", user.Name);
                    writer.WriteElementString("company", user.Company);
                    writer.WriteElementString("age", user.Age.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            // Виводимо файл
            XmlDocument doc = new XmlDocument();
            doc.Load("users.xml");
            foreach (XmlNode node in doc.DocumentElement)
            {
                string name = node["name"].InnerText;
                string company = node["company"].InnerText;
                int age = Int32.Parse(node["age"].InnerText);
                Console.WriteLine(string.Format("Користувач={0} працює в {1}, вік {2}", name, company, age));
            }
            // XDocument, XElement
            Console.WriteLine();
            XDocument xmlDoc = XDocument.Load("users.xml");
            foreach (XElement userElement in
           xmlDoc.Element("users").Elements("user"))
            {
                XElement nameAttribute = userElement.Element("name");
                XElement companyElement = userElement.Element("company");
                XElement ageElement = userElement.Element("age");
                if (nameAttribute != null && companyElement != null && ageElement
               != null)
                {
                    Console.WriteLine("Користувач: {0}", nameAttribute.Value);
                    Console.WriteLine("Компанія: {0}", companyElement.Value);
                    Console.WriteLine("Вік: {0}", ageElement.Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Перелік коммпаній, в котрих працюють користувачі, відсортовані за зростанням");
           
            var querySorted = xmlDoc.Descendants("user").Select(p =>
           p.Element("company").Value).OrderBy(p => p.Trim());
            foreach (var s in querySorted)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine("Фільтр за віком");
            IEnumerable<XElement> queryAge =
            from b in xmlDoc.Root.Elements("user")
            where (int)b.Element("age") == 42
            select b;
            Console.WriteLine(queryAge.FirstOrDefault().Element("name").Value);

            var items = from xe in xmlDoc.Element("users").Elements("user")
                        where xe.Element("company").Value == "Google"
                        select new User
                        {
                            Name = xe.Element("name").Value,
                            Company = xe.Element("company").Value,
                            Age = Int32.Parse(xe.Element("age").Value)
                        };
            Console.WriteLine();
            Console.WriteLine("Працюють в Google");
            foreach (var item in items)
                Console.WriteLine("{0} - {1} - {2}", item.Name, item.Company,
               item.Age);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
    class User
    {
        /// <summary>
        /// Им'я
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// <summary>
        /// Компанія
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// Вік
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Конструктор за замовчанням
        /// </summary>
        public User()
        {
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Им'я</param>
        /// <param name="company">Компанія</param>
        /// <param name="age">Вік</param>
        public User(string name, string company, int age)
        {
            Name = name;
            Company = company;
            Age = age;
        }
    }
}

