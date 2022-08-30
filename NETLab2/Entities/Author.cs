namespace NET_Lab2.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Organ { get; set; }

        public override bool Equals(object au)
        {
            var author = au as Author;
            return author.Name == this.Name
                && author.Surname == this.Surname
                && author.Secondname == this.Secondname
                && author.Organ == this.Organ;
        }

        public override int GetHashCode()
        {
            return new { this.Name, this.Surname, this.Secondname, this.Organ }.GetHashCode();
        }

        public override string ToString() => string.Format($"{Surname} {Name} {Secondname} - {Organ}");
    }
}