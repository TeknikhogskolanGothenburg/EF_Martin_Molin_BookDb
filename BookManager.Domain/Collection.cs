using System.Collections.Generic;

namespace BookManager.Domain
{
    public class Collection
    {
        public Collection()
        {
            BookCollections = new List<BookCollection>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<BookCollection> BookCollections { get; set; }
    }
}
