namespace BookManager.Domain
{
    public class Rating
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserRating { get; set; }
    }
}
