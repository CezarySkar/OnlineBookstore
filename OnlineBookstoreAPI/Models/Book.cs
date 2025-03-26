namespace OnlineBookstoreAPI.Models
{
    public class Book
    {
        public int Id {get; set;}
        public required string Title {get; set;}
        public required string Author {get; set;}
        public decimal Price {get;set;}
        public required string Genre {get; set;}
    }
}