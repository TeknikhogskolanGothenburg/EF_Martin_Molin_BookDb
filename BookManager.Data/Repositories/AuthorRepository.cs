using BookManager.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Data.Repositories
{
    public class AuthorRepository : GenericRepository<BookContext, Author>
    {
        public void AddBookToAuthor(Author inputAuthor, Book book)
        {
            using (var context = new BookContext())
            {
                var author = context.Authors
                    .FirstOrDefault(a => a.Id == inputAuthor.Id);
                author.Books.Add(book);
                context.SaveChanges();
            }
        }

        public async Task AddBookToAuthorAsync(Author inputAuthor, Book book) 
        {
            using (var context = new BookContext())
            {
                var author = await context.Authors
                    .FirstOrDefaultAsync(a => a.Id == inputAuthor.Id);
                author.Books.Add(book);
                await context.SaveChangesAsync();
            }
        }

        public void AddMultipleBooksToAuthor(Author inputAuthor, List<Book> books)
        {
            using (var context = new BookContext())
            {
                var author = context.Authors
                    .FirstOrDefault(a => a.Id == inputAuthor.Id);
                author.Books.AddRange(books);
                context.SaveChanges();
            }
        }

        public async Task AddMultipleBooksToAuthorAsync(Author inputAuthor, List<Book> books) 
        {
            using (var context = new BookContext())
            {
                var author = await context.Authors
                    .FirstOrDefaultAsync(a => a.Id == inputAuthor.Id);
                author.Books.AddRange(books);
                await context.SaveChangesAsync();
            }
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            using (var context = new BookContext())
            {
                var authors = context.Authors
                    .Where(a => a.FirstName.StartsWith(name))
                    .ToList();
                authors.AddRange(context.Authors
                .Where(a => a.LastName.StartsWith(name)).ToList());
                var result = authors.Distinct();
                return result;
            }
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name) 
        {
            using (var context = new BookContext())
            {
                var authors = await context.Authors
                    .Where(a => a.FirstName.StartsWith(name))
                    .ToListAsync();
                authors.AddRange(context.Authors
                .Where(a => a.LastName.StartsWith(name)).ToList());
                var result = authors.Distinct();
                return result;
            }
        }

        public void DeleteAuthorById(int inputId)
        {
            var id = inputId;
            using (var context = new BookContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM dbo.Authors WHERE Id = {0}", id);
            }
        }
    }
}
