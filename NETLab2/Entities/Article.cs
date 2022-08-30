namespace NET_Lab2.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }

        public override bool Equals(object ar)
        {
            var article = ar as Article;
            return article.Name == this.Name
                && article.ArticleId == this.ArticleId
                && article.AuthorId == this.AuthorId;
        }

        public override int GetHashCode()
        {
            return new { this.Name, this.ArticleId, this.AuthorId }.GetHashCode();
        }

        public override string ToString() => string.Format($"'{Name}' - {AuthorId}");
    }
}
