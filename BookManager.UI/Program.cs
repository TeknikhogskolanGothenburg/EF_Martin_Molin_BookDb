using BookManager.Data.Repositories;

namespace BookManager.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            BookRepository bookRepo = new BookRepository();
            var book = bookRepo.GetById(1);
            WorkAsync wa = new WorkAsync();
            //wa.GetBookInfoAndRating(book);
            wa.GetBookInfoRatingAndUsersAsync(book);
        }

    }
}
