namespace NET_Lab2.Entities
{
    class Author
    {
        public int AuthorId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Organ { get; set; }

        public override string ToString() => string.Format($"{Surname} {Name} {Secondname} - {Organ}");
    }
}