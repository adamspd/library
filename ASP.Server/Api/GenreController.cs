using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using Microsoft.AspNetCore.Mvc;
using ASP.Server.Model;
using ASP.Server.Data;

namespace ASP.Server.Api
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<List<Genre>> GetGenres()
        {
            return libraryDbContext.Genre.ToList();
        }

        public async Task<ActionResult<Genre>> GetGenreById(int genreId)
        {
            var genre = await libraryDbContext.Genre
                .Include(g => g.Books)
                .FirstOrDefaultAsync(genre => genre.Id == genreId);
            if (genre == null)
            {
                return NotFound();
            }
            return genre;
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create([FromBody] GenreDto createGenreDto)
        {
            // Create a new Genre object with the specified name
            var genre = new Genre { Name = createGenreDto.Name };

            // Add the new genre to the database
            libraryDbContext.Genre.Add(genre);
            await libraryDbContext.SaveChangesAsync();

            // Return a GenreDto object for the newly created genre
            var genreDto = new GenreDto { Name = genre.Name };
            return CreatedAtAction(nameof(GetGenreById), new { genreId = genre.Id }, genre);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, GenreDto updatedGenre)
        {
            var genre = await libraryDbContext.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            genre.Name = updatedGenre.Name;
            await libraryDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

