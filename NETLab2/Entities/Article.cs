namespace NET_Lab2.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }

        public override string ToString() => string.Format($"'{Name}' - {AuthorId}");
    }
}
