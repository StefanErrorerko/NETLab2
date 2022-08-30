using System.Collections.Generic;
using NET_Lab2.Entities;

namespace NET_Lab2.DataManagers
{
    public class Data
    {
        public List<Magazine> Mags;
        public List<Author> Authors;
        public List<Article> Articles;
        public List<EditorDoc> Docs;

        public Data()
        {
            Articles = new List<Article>();
            Mags = new List<Magazine>();
            Authors = new List<Author>();
            Docs = new List<EditorDoc>();
        }
    }
}
