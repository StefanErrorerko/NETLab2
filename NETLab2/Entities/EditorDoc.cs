using System;

namespace NET_Lab2.Entities
{
    public class EditorDoc
    {
        public int DocId { get; set; }
        public int ArticleId { get; set; }
        public int MagId { get; set; }
        public DateTime Date { get; set; }
        public override string ToString() => $"{Date.ToString("d")}: Article #{ArticleId} in Mag #{MagId}";
    }
}
