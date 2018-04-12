using System.Collections.Generic;

namespace BookManager.Domain
{
    public class Genre
    {
        public Genre()
        {
            BookGenres = new List<BookGenre>();
        }
        public int Id { get; set; }
        public string Category { get; set; }

        public List<BookGenre> BookGenres { get; set; }
    }
}
