namespace OnlineBookstoreAPI.DTOs
{
    public class CreateUserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }
    }
}
