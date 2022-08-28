using System.Collections.Generic;
using NET_Lab2.Instruments;
using NET_Lab2.Entities;

namespace NET_Lab2
{
    internal class Data
    {
        internal List<Magazine> Mags;
        internal List<Author> Authors;
        internal List<Article> Articles;
        internal List<EditorDoc> Docs;

        internal List<Author> Au1;
        internal List<Author> Au2;

        internal void MagWithCheck(List<Magazine> mags)
        {
            foreach (var mag in mags)
            {
                DateTimeAccuracyMonitor.CheckDate(mag.Est);
            }
        }

        internal void DocWithCheck(List<EditorDoc> docs)
        {
            foreach (var doc in docs)
            {
                DateTimeAccuracyMonitor.CheckDate(doc.Date);
            }
        }
    }
}
