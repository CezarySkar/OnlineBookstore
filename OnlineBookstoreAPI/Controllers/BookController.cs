using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookstoreAPI.DTOs;
using OnlineBookstoreAPI.Data;
using OnlineBookstoreAPI.Models;
using OnlineBookstoreAPI.Services;

namespace OnlineBookstoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public BooksController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _ctx.Books.ToListAsync();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                Genre = createBookDto.Genre,
                Price = createBookDto.Price
            };
            await _ctx.Books.AddAsync(book);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(AddBook), "sdasd", book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            bool searchRes = await _ctx.Books.AnyAsync(b => b.Id == id);
            if (!searchRes)
            {
                return BadRequest(new { error = "Book doesn't exist!" });
            }
            var book = new Book 
            {
                Id = id,
                Title = updateBookDto.Title,
                Author = updateBookDto.Author,
                Genre = updateBookDto.Genre,
                Price = updateBookDto.Price
            };
            _ctx.Entry(book).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _ctx.Books.FindAsync(id);
            if (book == null) return NotFound();

            _ctx.Books.Remove(book);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}