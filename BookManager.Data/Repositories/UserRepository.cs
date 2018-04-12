using BookManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Data.Repositories
{
    public class UserRepository : GenericRepository<BookContext, User>
    {        
        public void AddUserCollection(User inputUser, Collection collection)
        {
            using (var context = new BookContext())
            {
                var user = context.Users.Where(u => u.Id == inputUser.Id)
                    .FirstOrDefault();
                user.Collections.Add(collection);
                context.SaveChanges();
            }
        }

        public async Task AddUserCollectionAsync(User inputUser, Collection collection)
        {
            using (var context = new BookContext())
            {
                var user = await context.Users.Where(u => u.Id == inputUser.Id)
                    .FirstOrDefaultAsync();
                user.Collections.Add(collection);
                await context.SaveChangesAsync();
            }
        }

        public void AddUserRating(User user, Book book, int rating)
        {
            using (var context = new BookContext())
            {
                context.Add(new Rating { UserRating = rating, UserId = user.Id, BookId = book.Id });
                context.SaveChanges();
            }
        }

        public async Task AddUserRatingAsync(int rating, User user, Book book)
        {
            using (var context = new BookContext())
            {
                context.Add(new Rating { UserRating = rating, UserId = user.Id, BookId = book.Id });
                await context.SaveChangesAsync();
            }
        }

        public List<Collection> GetCollectionsByUser(User inputUser)
        {
            var collections = Context.Collections
                .Include(c => c.User)
                .Where(u => u.UserId == inputUser.Id)
                .ToList();
            return collections;
        }

        public async Task<List<Collection>> GetCollectionsByUserAsync(User inputUser) 
        {
            var collections = await Context.Collections
                .Include(c => c.User)
                .Where(u => u.UserId == inputUser.Id)
                .ToListAsync();
            Console.WriteLine("First!");
            return collections;
        }

        public void UpdateUserRating(User user, Book book, int rating) 
        {
            using (var context = new BookContext())
            {
                var updateRating = context.Ratings
                    .Where(r => r.UserId == user.Id && r.BookId == book.Id)
                    .FirstOrDefault();
                updateRating.UserRating = rating;
                context.SaveChanges();
            }
        }

        public async Task UpdateUserRatingAsync(int rating, User user, Book book) 
        {
            using (var context = new BookContext())
            {
                var updateRating = await context.Ratings
                    .Where(r => r.UserId == user.Id && r.BookId == book.Id)
                    .FirstOrDefaultAsync();
                updateRating.UserRating = rating;
                await context.SaveChangesAsync();
            }
        }

        public void DeleteUserById(int inputId)
        {
            var id = inputId;
            using (var context = new BookContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM dbo.Users WHERE Id = {0}", id);
            }
        }

        public void DeleteUserRating(User user, Book book)
        {
            using (var context = new BookContext())
            {
                var rating = context.Ratings.FirstOrDefault(r => r.User == user &&
                r.Book == book);
                context.Ratings.Remove(rating);
                context.SaveChanges();
            }
        }

        public async Task DeleteUserRatingAsync(User user, Book book)
        {
            using (var context = new BookContext())
            {
                var rating = await context.Ratings.FirstOrDefaultAsync(r => r.User == user &&
                r.Book == book);
                context.Ratings.Remove(rating);
                await context.SaveChangesAsync();
            }
        }
    }
}
