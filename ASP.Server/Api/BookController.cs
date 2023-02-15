using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.Data;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace ASP.Server.Api
{

    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        // Methode a ajouter : 
        // - GetBooks
        //   - Entrée: Optionel -> Liste d'Id de genre, limit -> defaut à 10, offset -> défaut à 0
        //     Le but de limit et offset est dé créer un pagination pour ne pas retourner la BDD en entier a chaque appel
        //   - Sortie: Liste d'object contenant uniquement: Auteur, Genres, Titre, Id, Prix
        //     la liste restourner doit être compsé des élément entre <offset> et <offset + limit>-
        //     Dans [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20] si offset=8 et limit=5, les élément retourner seront : 8, 9, 10, 11, 12

        // - GetBook
        //   - Entrée: Id du livre
        //   - Sortie: Object livre entier

        // - GetGenres
        //   - Entrée: Rien
        //   - Sortie: Liste des genres

        // Aide:
        // Pour récupéré un objet d'une table :
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.First()
        // Pour récupéré des objets d'une table :
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.ToList()
        // Pour faire une requète avec filtre:
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Skip().<Selecteurs>
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Take().<Selecteurs>
        //   - libraryDbContext.MyObjectCollection.<Selecteurs>.Where(x => x == y).<Selecteurs>
        // Pour récupérer une 2nd table depuis la base:
        //   - .Include(x => x.yyyyy)
        //     ou yyyyy est la propriété liant a une autre table a récupéré
        //
        // Exemple:
        //   - Ex: libraryDbContext.MyObjectCollection.Include(x => x.yyyyy).Where(x => x.yyyyyy.Contains(z)).Skip(i).Take(j).ToList()


        // Je vous montre comment faire la 1er, a vous de la compléter et de faire les autres !

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromQuery] int genreId = -1, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            var queryable = libraryDbContext.Books.AsQueryable();
            var bookListQueryable = genreId != -1 ?
                queryable.Where(b => b.Genres.Any(g => genreId == g.Id)) :
                queryable;
            var totalBooks = await bookListQueryable.CountAsync();

            var bookList = await bookListQueryable
                .Include(b => b.Genres)
                .OrderBy(b => b.Id)
                .Skip(offset - 1)
                .Take(limit)
                .ToListAsync();

            var paginationHeader = $"{offset}-{offset + bookList.Count - 1}/{totalBooks}";
            Response.Headers.Add("Pagination", paginationHeader);

            return Ok(bookList.Select(b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price,
                Genres = b.Genres
            }));
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBook(int bookId)
        {
            var book = await libraryDbContext.Books
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] BookDto newBookDto)
        {
            // Create a new Book object using the properties of the BookDto object
            var newBook = new Book
            {
                Title = newBookDto.Title,
                Author = newBookDto.Author,
                Content = newBookDto.Content,
                Price = newBookDto.Price ?? 0,
                Genres = new List<Genre>()
            };

            // Add genres to the new book
            if (newBookDto.GenreIds != null)
            {
                foreach (var genreId in newBookDto.GenreIds)
                {
                    var genre = await libraryDbContext.Genre.FindAsync(genreId);
                    if (genre == null)
                    {
                        genre = new Genre { Id = genreId };
                        libraryDbContext.Genre.Add(genre);
                    }
                    newBook.Genres.Add(genre);
                }
            }

            // Add the new book to the context and save changes
            libraryDbContext.Books.Add(newBook);
            await libraryDbContext.SaveChangesAsync();

            // Return the newly added book with its ID set
            return CreatedAtAction(nameof(GetBook), new { bookId = newBook.Id }, newBook);
        }

        [HttpPut]
        public async Task<ActionResult<Book>> UpdateBook(int bookId, [FromBody] BookDto updatedBookDto)
        {
            // Retrieve the book from the database
            var bookToUpdate = await libraryDbContext.Books
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            // Update the book with the properties of the updated BookDto object
            bookToUpdate.Title = updatedBookDto.Title;
            bookToUpdate.Author = updatedBookDto.Author;
            bookToUpdate.Content = updatedBookDto.Content;
            bookToUpdate.Price = updatedBookDto.Price ?? 0;
            bookToUpdate.Genres.Clear();

            // Add genres to the book
            if (updatedBookDto.GenreIds != null)
            {
                foreach (var genreId in updatedBookDto.GenreIds)
                {
                    var genre = await libraryDbContext.Genre.FindAsync(genreId);
                    if (genre == null)
                    {
                        genre = new Genre { Id = genreId };
                        libraryDbContext.Genre.Add(genre);
                    }
                    bookToUpdate.Genres.Add(genre);
                }
            }

            // Save changes to the database
            await libraryDbContext.SaveChangesAsync();

            // Return the updated book
            return Ok(bookToUpdate);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            // Find the book to delete
            var book = await libraryDbContext.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            // Remove the book from the context and save changes
            libraryDbContext.Books.Remove(book);
            await libraryDbContext.SaveChangesAsync();

            // Return a 204 No Content response
            return NoContent();
        }
    }
}

