namespace BookManager.Domain
{
    public class BookCollection
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
