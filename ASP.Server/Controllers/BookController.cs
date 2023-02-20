using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YamlDotNet.Core.Tokens;
using System.Net;
using Microsoft.EntityFrameworkCore.Query;

namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Titre")]
        public String Title { get; set; }

        [Required]
        [Display(Name = "Contenu")]
        public String Content { get; set; }

        [Required]
        [Display(Name = "Prix")]
        public Double Price { get; set; }

        [Required]
        [Display(Name = "Auteur")]
        public String Author { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init; }
    }


    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        public ActionResult<IEnumerable<Book>> List(string author, string genre)
        {
            var query = libraryDbContext.Books.AsQueryable();

            // If an author is specified, filter the books by author name
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Name == author);
            }

            // If a genre is specified, filter the books by genre name
            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genres.Any(g => g.Name == genre));
            }

            // Retrieve the filtered list of books from the database
            List<Book> listBooks = query.ToList();

            // Get the list of genres and authors to display in the view
            var genres = libraryDbContext.Genre.OrderBy(g => g.Name).ToList();
            var authors = libraryDbContext.Authors.OrderBy(a => a.Name).Select(a => a.Name).ToList();

            // Pass the filtered list of books, the list of genres and the list of authors to the view
            ViewBag.Authors = authors;
            ViewData["Genres"] = genres;

            return View(listBooks);
        }



        public ActionResult<List<Book>> DeleteBook(int id)
        {
            Book book= libraryDbContext.Books.FirstOrDefault(x => x.Id == id);
            libraryDbContext.Books.Remove(book);
            libraryDbContext.SaveChanges();

            return View("List", libraryDbContext.Books.ToList());
        }

        public ActionResult<Book> Edit(int id)
        {
            Book book = libraryDbContext.Books.FirstOrDefault(x => x.Id == id);

             return View(book);
        }

        public ActionResult<CreateBookModel> Update(int id, CreateBookModel createBookModel = null)
        {
            Book book = libraryDbContext.Books.Include(b => b.Genres).Include(b => b.Author).FirstOrDefault(b => b.Id == id);

            List<int> genresId = new List<int>();

            foreach (var genre in book.Genres)
            {
                genresId.Add(genre.Id);
            }

            CreateBookModel bookModel = new CreateBookModel()
            {
                Title = book.Title,
                AllGenres = libraryDbContext.Genre.ToList(),
                Author = book.Author.Name,
                Content = book.Content,
                Genres = genresId,
                Price = book.Price
            };

            if (createBookModel.Title != null)
            {
                book.Title = createBookModel.Title;
                book.Price = createBookModel.Price;
                book.Author.Name = createBookModel.Author;
                book.Content = createBookModel.Content;
                book.Genres.Clear();

                foreach (var genreId in createBookModel.Genres)
                {
                    var genre = libraryDbContext.Genre.FirstOrDefault(genre => genre.Id == genreId);
                    if (genre == null) { return View(bookModel); }

                    book.Genres.Add(genre);
                }
                var genres = libraryDbContext.Genre.OrderBy(g => g.Name).ToList();
                var authors = libraryDbContext.Authors.OrderBy(a => a.Name).Select(a => a.Name).ToList();

                // Pass the filtered list of books, the list of genres and the list of authors to the view
                ViewBag.Authors = authors;
                ViewData["Genres"] = genres;

                libraryDbContext.SaveChanges();
                return View("List", libraryDbContext.Books.ToList());
            }

            return View(bookModel);
        }


        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres


                var genre = libraryDbContext.Genre.Where(genreDb => book.Genres.Contains( genreDb.Id)).ToList();
                // new Book() { Genres = genre, Content = book.Content };





                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
                var author = libraryDbContext.Authors.FirstOrDefault(a => a.Name == book.Author);
                if (author == null)
                {
                    // If the author doesn't exist, create a new Author object with the specified name
                    author = new Author { Name = book.Author };
                    libraryDbContext.Authors.Add(author);
                }
                libraryDbContext.Add(new Book() { Genres = genre, Content = book.Content , Price = book.Price, Title = book.Title, Author = author});
                libraryDbContext.SaveChanges();
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = libraryDbContext.Genre.ToList() });
        }
    }
}
