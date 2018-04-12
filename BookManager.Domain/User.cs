using System.Collections.Generic;

namespace BookManager.Domain
{
    public class User
    {
        public User()
        {
            Collections = new List<Collection>();
            Ratings = new List<Rating>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public List<Collection> Collections { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
