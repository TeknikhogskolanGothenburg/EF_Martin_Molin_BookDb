using BookManager.Data.Repositories;
using BookManager.Domain;
using System;
using System.Linq;

namespace BookManager.UI
{
    public class WorkAsync
    {
        // Anropen mot Ratings och Users kan bli tunga med flera tusen 
        // ratings och användare, därför har jag valt att jobba asynkront.
        // Databasen är till för att användas med ett ASP.NET MVC projekt och 
        // EF Core är inte threadsafe ännu vilket var till fördel
        // för asynkront.

        public void GetBookInfoRatingAndUsersAsync(Book inputBook)
        {
            BookRepository bookRepo = new BookRepository();
            AuthorRepository authorRepo = new AuthorRepository();
            GenreRepository genreRepository = new GenreRepository();
            CollectionRepository collectionRepo = new CollectionRepository();
            var book = bookRepo.GetById(1);
            var bookRatingsAsync = bookRepo.GetAllUserRatingsByBookAsync(book);
            var usersAsync = bookRepo.GetAllBookOwnersAsync(book);
            var genreAsync = bookRepo.GetBookGenresAsync(book);
            var authorAsync = authorRepo.GetByIdAsync(book.Id);
            var author = authorAsync.Result;
            var genres = genreAsync.Result;
            var users = usersAsync.Result;
            var bookRatings = bookRatingsAsync.Result;

            Console.WriteLine("Title: " + book.Title + "\nAuthor: " + author.FirstName + " " + author.LastName);
            Console.Write("Genres: ");
            foreach (var genre in genres)
            {
                Console.Write(" / " + genre.Category);
            }
            Console.WriteLine("\nAverage Score: " + bookRatings.Values.Average() + " / 10");
            Console.WriteLine("In users collection: ");
            foreach (var user in users)
            {
                Console.WriteLine("- " + user.Username);
            }
        }


        public void GetBookInfoAndRating(Book inputBook)
        {

            BookRepository bookRepo = new BookRepository();
            AuthorRepository authorRepo = new AuthorRepository();
            GenreRepository genreRepository = new GenreRepository();
            CollectionRepository collectionRepo = new CollectionRepository();
            var book = bookRepo.GetById(1);
            var bookRatingsAsync = bookRepo.GetAllUserRatingsByBookAsync(book);
            var genreAsync = bookRepo.GetBookGenresAsync(book);
            var authorAsync = authorRepo.GetByIdAsync(book.Id);
            var author = authorAsync.Result;
            var genres = genreAsync.Result;
            var bookRatings = bookRatingsAsync.Result;

            Console.WriteLine("Title: " + book.Title + "\nAuthor: " + author.FirstName + " " + author.LastName);
            Console.Write("Genres: ");
            foreach (var genre in genres)
            {
                Console.Write(" / " + genre.Category);
            }
            Console.WriteLine("\nAverage Score: " + bookRatings.Values.Average() + " / 10");
        }
    }
}
