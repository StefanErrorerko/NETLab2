using System.Collections.Generic;
using NET_Lab2.Instruments;
using NET_Lab2.Entity;

namespace NET_Lab2
{
    internal class Data
    {
        internal List<Magazine> Mags;
        internal List<Author> Authors;
        internal List<Article> Articles;
        internal List<EditorDoc> Docs;

        internal List<Author> au1;
        internal List<Author> au2;

        internal void MagWithCheck(List<Magazine> mags)
        {
            foreach (Magazine m in mags) DateTimeAccuracyMonitor.CheckDate(m.Est);
        }

        internal void DocWithCheck(List<EditorDoc> docs)
        {
            foreach (EditorDoc d in docs) DateTimeAccuracyMonitor.CheckDate(d.Date);
        }
    }
}
