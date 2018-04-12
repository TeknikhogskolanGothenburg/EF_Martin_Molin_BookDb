using System.Collections.Generic;

namespace BookManager.Domain
{
    public class Book
    {
        public Book()
        {
            BookGenres = new List<BookGenre>();
            BookCollections = new List<BookCollection>();
            Ratings = new List<Rating>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublicationYear { get; set; }
        public long ISBN { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BookGenre> BookGenres { get; set; }
        public List<BookCollection> BookCollections { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
