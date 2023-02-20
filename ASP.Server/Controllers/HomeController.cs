using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.Data;

namespace ASP.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryDbContext libraryDbContext;

        public HomeController(ILogger<HomeController> logger, LibraryDbContext libraryDbContext)
        {
            _logger = logger;
            this.libraryDbContext = libraryDbContext;
        }

        public IActionResult Index()
        {
            var totalBooks = libraryDbContext.Books.Count();
            var bookCountsByAuthor = libraryDbContext.Books
                .GroupBy(b => b.Author)
                .Select(g => new AuthorBookCount
                    {
                        Author = g.Key.Name,
                        BookCount = g.Count()
                    })
                .ToList();

            // Calculate the maximum number of words in a book
            var maxWords = libraryDbContext.Books.Max(b => b.Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);


            // Calculate the minimum number of words in a book
            var minWords = libraryDbContext.Books.Min(b => b.Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);

            // Calculate the median number of words in a book
            var medianWords = libraryDbContext.Books.OrderBy(b => b.Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length)
                .Skip((libraryDbContext.Books.Count() - 1) / 2)
                .Take(2 - libraryDbContext.Books.Count() % 2)
                .Average(b => b.Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);

            // Calculate the average number of words in a book
            var avgWords = libraryDbContext.Books.Average(b => b.Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);

            ViewBag.TotalBooks = totalBooks;
            ViewBag.BookCountsByAuthor = bookCountsByAuthor;
            ViewBag.MaxWords = maxWords;
            ViewBag.MinWords = minWords;
            ViewBag.MedianWords = medianWords;
            ViewBag.AvgWords = avgWords;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
