using BookManager.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Data.Repositories
{
    public class CollectionRepository : GenericRepository<BookContext, Collection>
    {
        public void AddBookToCollection(Collection collection, Book book)
        {
            using (var context = new BookContext())
            {
                context.Add(new BookCollection { CollectionId = collection.Id, BookId = book.Id });
                context.SaveChanges();
            }
        }

        public async Task AddBookToCollectionAsync(Collection collection, Book book)
        {
            using (var context = new BookContext())
            {
                await context.AddAsync(new BookCollection { CollectionId = collection.Id, BookId = book.Id });
                await context.SaveChangesAsync();
            }
        }

        public void AddMultipleBooksToCollection(Collection collection, List<Book> books)
        {
            using (var context = new BookContext())
            {
                books.ForEach(b => context.Add(new BookCollection { CollectionId = collection.Id, BookId = b.Id }));
                context.SaveChanges();
            }
        }

        public async Task AddMultipleBooksToCollectionAsync(Collection collection, List<Book> books)
        {
            using (var context = new BookContext())
            {
                books.ForEach(b => context.Add(new BookCollection { CollectionId = collection.Id, BookId = b.Id }));
                await context.SaveChangesAsync();
            }
        }

        public List<Book> GetAllBooksInCollection(Collection inputCollection)
        {
            var books = new List<Book>();
            using (var context = new BookContext())
            {
                var collections = context.Collections.Where(c => c.Id == inputCollection.Id)
                    .Include(c => c.BookCollections)
                    .ThenInclude(bc => bc.Book)
                    .FirstOrDefault();
                foreach (var bookCollection in collections.BookCollections)
                {
                    books.Add(bookCollection.Book);
                }
                return books;
            }
        }

        public async Task<List<Book>> GetAllBooksInCollectionAsync(Collection inputCollection)
        {
            var books = new List<Book>();
            using (var context = new BookContext())
            {
                var collections = await context.Collections.Where(c => c.Id == inputCollection.Id)
                    .Include(c => c.BookCollections)
                    .ThenInclude(bc => bc.Book)
                    .FirstOrDefaultAsync();
                foreach (var bookCollection in collections.BookCollections)
                {
                    books.Add(bookCollection.Book);
                }
                return books;
            }
        }

        public void DeleteBookFromCollection(Collection collection, Book book)
        {
            using (var context = new BookContext())
            {
                var bookCollection = context.BookCollections
                    .FirstOrDefault(bc => bc.CollectionId == collection.Id &&
                    bc.BookId == book.Id);
                context.Remove(bookCollection);
                context.SaveChanges();
            }
        }

        public async Task DeleteBookFromCollectionAsync(Collection collection, Book book)
        {
            using (var context = new BookContext())
            {
                var bookCollection = await context.BookCollections
                    .FirstOrDefaultAsync(bc => bc.CollectionId == collection.Id &&
                    bc.BookId == book.Id);
                context.Remove(bookCollection);
                await context.SaveChangesAsync();
            }
        }

        public void DeleteMultipleBooksFromCollection(Collection collection, List<Book> inputBooks)
        {
            var books = inputBooks;
            using (var context = new BookContext())
            {
                foreach (var book in books)
                {
                    var bookCollection = context.BookCollections
                        .FirstOrDefault(bc => bc.CollectionId == collection.Id &&
                        bc.BookId == book.Id);
                    context.Remove(bookCollection);
                    context.SaveChanges();
                }
            }
        }

        public async Task DeleteMultipleBooksFromCollectionAsync(Collection collection, List<Book> inputBooks)
        {
            var books = inputBooks;
            using (var context = new BookContext())
            {
                foreach (var book in books)
                {
                    var bookCollection = await context.BookCollections
                        .FirstOrDefaultAsync(bc => bc.CollectionId == collection.Id &&
                        bc.BookId == book.Id);
                    context.Remove(bookCollection);
                    await context.SaveChangesAsync();
                }
            }
        }
                     
        public void DeleteCollectionById(int inputId) //Test!
        {
            var id = inputId;
            using (var context = new BookContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM dbo.Collections WHERE Id = {0}", id);
            }
        }
    }
}
