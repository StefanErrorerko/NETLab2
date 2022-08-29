namespace NET_Lab2.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Organ { get; set; }

        //public override bool Equals(Author author2)
        //{
        //    return author2.Name == this.Name 
        //        && author2.Surname == this.Surname 
        //        && author2.Secondname == this.Secondname 
        //        && author2.Organ == this.Organ;
        //}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString() => string.Format($"{Surname} {Name} {Secondname} - {Organ}");
    }
}