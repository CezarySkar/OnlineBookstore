namespace OnlineBookstoreAPI.DTOs
{
    public class UpdateBookDto
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public decimal Price { get; set; }
        public required string Genre { get; set; }
    }
}
