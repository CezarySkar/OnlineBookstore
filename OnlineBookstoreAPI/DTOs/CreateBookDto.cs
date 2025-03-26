using System.Runtime.CompilerServices;

namespace OnlineBookstoreAPI.DTOs
{
    public class CreateBookDto
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public decimal Price { get; set; }
        public required string Genre { get; set; }
    }
}
