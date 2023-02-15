using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using Microsoft.AspNetCore.Mvc;
using ASP.Server.Model;


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

        public ActionResult<Genre> GetGenreById(int genreId)
        {
            var queryable = libraryDbContext.Genre.AsQueryable();
            if (queryable.Select(genre => genre.Id == genreId) == null)
            {
                return NotFound();
            }
            return queryable.FirstOrDefault(genre => genre.Id == genreId);
        }
    }
}

