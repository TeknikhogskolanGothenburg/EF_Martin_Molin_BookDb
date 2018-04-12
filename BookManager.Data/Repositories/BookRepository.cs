using BookManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Data.Repositories
{
    public class BookRepository : GenericRepository<BookContext, Book>
    {
        public void AddBookToGenre(Book inputBook, Genre inputGenre)
        {
            using (var context = new BookContext())
            {
                context.Add(new BookGenre { BookId = inputBook.Id, GenreId = inputGenre.Id });
                context.SaveChanges();
            }
        }

        public async Task AddBookToGenreAsync(Book inputBook, Genre inputGenre)
        {
            using (var context = new BookContext())
            {
                context.Add(new BookGenre { BookId = inputBook.Id, GenreId = inputGenre.Id });
                await context.SaveChangesAsync();
            }
        }

        public void AddMultipleBooksToGenre(List<Book> inputBooks, Genre genre)
        {
            var books = inputBooks;
            using (var context = new BookContext())
            {
                foreach (var book in books)
                {
                    context.Add(new BookGenre { BookId = book.Id, GenreId = genre.Id });
                }
                context.SaveChanges();
            }
        }

        public async Task AddMultipleBooksToGenreAsync(List<Book> inputBooks, Genre genre)
        {
            var books = inputBooks;
            using (var context = new BookContext())
            {
                foreach (var book in books)
                {
                    await context.AddAsync(new BookGenre { BookId = book.Id, GenreId = genre.Id });
                }
                await context.SaveChangesAsync();
            }
        }

        public List<User> GetAllBookOwners(Book book)
        {
            var users = new List<User>();
            using (var context = new BookContext())
            {
                var result = context.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.BookCollections)
                    .ThenInclude(bc => bc.Collection)
                    .ThenInclude(c => c.User)
                    .ToList();

                var bookUsers = result.Distinct();

                foreach (var bookUser in bookUsers)
                {
                    foreach (var bookCollection in bookUser.BookCollections)
                    {
                        users.Add(bookCollection.Collection.User);
                    }
                }
            }
            return users;
        }

        public async Task<List<User>> GetAllBookOwnersAsync(Book book)
        {
            var users = new List<User>();
            using (var context = new BookContext())
            {
                var result = await context.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.BookCollections)
                    .ThenInclude(bc => bc.Collection)
                    .ThenInclude(c => c.User)
                    .ToListAsync();


                var bookUsers = result.Distinct();

                foreach (var bookUser in bookUsers)
                {
                    foreach (var bookCollection in bookUser.BookCollections)
                    {
                        users.Add(bookCollection.Collection.User);
                    }
                }
            }
            return users;
        }

        public Dictionary<User, int> GetAllUserRatingsByBook(Book book)
        {
            var result = new Dictionary<User, int>();
            using (var context = new BookContext())
            {
                var userRatings = context.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.Ratings)
                    .ThenInclude(r => r.User)
                    .ToList();
                foreach (var userRating in userRatings)
                {
                    foreach (var user in userRating.Ratings)
                    {
                        result.Add(user.User, user.UserRating);
                    }
                }
                return result;
            }
        }

        public async Task<Dictionary<User, int>> GetAllUserRatingsByBookAsync(Book book)
        {
            var result = new Dictionary<User, int>();
            using (var context = new BookContext())
            {
                var userRatings = await context.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.Ratings)
                    .ThenInclude(r => r.User)
                    .ToListAsync();
                foreach (var userRating in userRatings)
                {
                    foreach (var user in userRating.Ratings)
                    {
                        result.Add(user.User, user.UserRating);
                    }
                }
                return result;
            }
        }

        public decimal GetAverageRatingByBook(Book book)
        {
            using (var context = new BookContext())
            {
                var sum = new decimal();
                var ratings = context.Books
                    .Include(r => r.Ratings)
                    .FirstOrDefault(b => b.Id == book.Id);
                foreach (var rating in ratings.Ratings)
                {
                    sum += rating.UserRating;
                }
                sum /= ratings.Ratings.Count();
                sum = Math.Round(sum, 2);
                return sum;
            }
        }

        public async Task<decimal> GetAverageRatingByBookAsync(Book book)
        {
            using (var context = new BookContext())
            {
                var sum = new decimal();
                var ratings = await context.Books
                    .Include(r => r.Ratings)
                    .FirstOrDefaultAsync(b => b.Id == book.Id);
                foreach (var rating in ratings.Ratings)
                {
                    sum += rating.UserRating;
                }
                sum /= ratings.Ratings.Count();
                sum = Math.Round(sum, 2);
                return sum;
            }
        }

        public List<Genre> GetBookGenres(Book book)
        {
            var results = new List<Genre>();
            using (var context = new BookContext())
            {
                var genres = context.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .ToList();
                foreach (var genre in genres)
                {
                    foreach (var category in genre.BookGenres)
                    {
                        results.Add(category.Genre);
                    }
                }
            }
            return results;
        }

        public async Task<List<Genre>> GetBookGenresAsync(Book book)
        {
            var results = new List<Genre>();
            using (var context = new BookContext())
            {
                var genres = await context.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.BookGenres)
                    .ThenInclude(bg => bg.Genre)
                    .ToListAsync();
                foreach (var genre in genres)
                {
                    foreach (var category in genre.BookGenres)
                    {
                        results.Add(category.Genre);
                    }
                }
            }
            return results;
        }

        public List<Book> GetBooksByAuthor(Author author)
        {
            using (var context = new BookContext())
            {
                var books = context.Books
                    .Include(b => b.Author)
                    .Where(a => a.AuthorId == author.Id)
                    .ToList();
                return books;
            }
        }

        public async Task<List<Book>> GetBooksByAuthorIdAsync(Author author)
        {
            using (var context = new BookContext())
            {
                var books = await context.Books
                    .Include(b => b.Author)
                    .Where(a => a.AuthorId == author.Id)
                    .ToListAsync();
                return books;
            }
        }

        public List<Book> GetBooksByCountry(string country)
        {
            using (var context = new BookContext())
            {
                var books = context.Books
                    .Include(b => b.Author)
                    .Where(a => a.Author.Country == country)
                    .ToList();
                return books;
            }
        }

        public async Task<List<Book>> GetBooksByCountryAsync(string country)
        {
            using (var context = new BookContext())
            {
                var books = await context.Books
                    .Include(b => b.Author)
                    .Where(a => a.Author.Country == country)
                    .ToListAsync();
                return books;
            }
        }

        public List<Book> GetBooksByGenre(Genre inputGenre)
        {
            var result = new List<Book>();
            using (var context = new BookContext())
            {
                var genres = context.Genres
                    .Where(g => g.Category == inputGenre.Category)
                    .Include(g => g.BookGenres)
                    .ThenInclude(bg => bg.Book)
                    .ToList();
                foreach (var genre in genres)
                {
                    foreach (var bookGenre in genre.BookGenres)
                    {
                        result.Add(bookGenre.Book);
                    }
                }
                return result;
            }
        }

        public async Task<List<Book>> GetBooksByGenreAsync(Genre inputGenre)
        {
            var result = new List<Book>();
            using (var context = new BookContext())
            {
                var genres = await context.Genres
                    .Where(g => g.Category == inputGenre.Category)
                    .Include(g => g.BookGenres)
                    .ThenInclude(bg => bg.Book)
                    .ToListAsync();
                foreach (var genre in genres)
                {
                    foreach (var bookGenre in genre.BookGenres)
                    {
                        result.Add(bookGenre.Book);
                    }
                }
                return result;
            }
        }

        public List<Book> GetBooksByYear(int year)
        {
            using (var context = new BookContext())
            {
                var books = context.Books
                    .Where(b => b.PublicationYear == year)
                    .ToList();
                return books;
            }
        }

        public async Task<List<Book>> GetBooksByYearAsync(int year)
        {
            using (var context = new BookContext())
            {
                var books = await context.Books
                    .Where(b => b.PublicationYear == year)
                    .ToListAsync();
                return books;
            }
        }

        public List<Book> GetBooksByYear(int fromYear, int toYear)
        {
            using (var context = new BookContext())
            {
                var books = context.Books
                    .Where(b => b.PublicationYear >= fromYear &&
                    b.PublicationYear <= toYear)
                    .ToList();
                return books;
            }
        }

        public async Task<List<Book>> GetBooksByYearAsync(int fromYear, int toYear)
        {
            using (var context = new BookContext())
            {
                var books = await context.Books
                    .Where(b => b.PublicationYear >= fromYear &&
                    b.PublicationYear <= toYear)
                    .ToListAsync();
                return books;
            }
        }

        public void ChangeBookAuthor(Book inputBook, Author author)
        {
            using (var context = new BookContext())
            {
                var book = context.Books
                    .FirstOrDefault(b => b.Id == inputBook.Id);
                book.AuthorId = author.Id;
                context.SaveChanges();
            }
        }

        public async Task ChangeBookAuthorAsync(Book inputBook, Author author)
        {
            using (var context = new BookContext())
            {
                var book = context.Books
                    .FirstOrDefault(b => b.Id == inputBook.Id);
                book.AuthorId = author.Id;
                await context.SaveChangesAsync();
            }
        }

        public void DeleteGenreFromBook(Book inputBook, Genre inputGenre)
        {
            using (var context = new BookContext())
            {
                var bookGenre = context.BookGenres
                    .FirstOrDefault(bg => bg.BookId == inputBook.Id &&
                    bg.GenreId == inputGenre.Id);
                context.Remove(bookGenre);
                context.SaveChanges();
            }
        }

        public async Task DeleteGenreFromBookAsync(Book inputBook, Genre inputGenre)
        {
            using (var context = new BookContext())
            {
                var bookGenre = await context.BookGenres
                    .Where(bg => bg.BookId == inputBook.Id &&
                    bg.GenreId == inputGenre.Id)
                    .FirstOrDefaultAsync();
                context.Remove(bookGenre);
                await context.SaveChangesAsync();
            }
        }

        public void DeleteBookById(int inputId) //Test!
        {
            var id = inputId;
            using (var context = new BookContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM dbo.Books WHERE Id = {0}", id);
            }
        }
    }
}
