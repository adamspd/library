using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP.Server.Controllers
{
    public class CreateGenreModel
    {
        [Required]
        [Display(Name = "Intitulé du Genre")]
        public String Name { get; set; }

    }


    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public GenreController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Genre>> List()
        {
            List<Genre> ListGenres = libraryDbContext.Genre.ToList();
            return View(ListGenres);
        }

        public ActionResult<CreateGenreModel> CreateGenre(CreateGenreModel genre)
        {
            if (ModelState.IsValid)
            {
                libraryDbContext.Add(new Genre() { Name = genre.Name });
                libraryDbContext.SaveChanges();
            }
            return View("Create", new CreateGenreModel());
        }
      
    }
}
