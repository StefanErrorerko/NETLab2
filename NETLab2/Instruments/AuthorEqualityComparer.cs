using System.Collections.Generic;
using NET_Lab2.Entities;

namespace NET_Lab2.Instruments
{
    class AuthorEqualityComparer : IEqualityComparer<Author>
    {
        public bool Equals(Author x, Author y)
        {
            return x.Name == y.Name && x.Surname == y.Surname && x.Secondname == y.Secondname && x.Organ == y.Organ;
        }
        public int GetHashCode(Author obj)
        {
            return new { obj.Name, obj.Surname, obj.Secondname, obj.Organ }.GetHashCode();
        } 
    }
}
