using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller, Programming;
            bookDbContext.Genre.AddRange(
                SF = new Genre() { Name = "SF" },
                Classic = new Genre() { Name = "Classic" },
                Romance = new Genre() { Name = "Romance" },
                Thriller = new Genre() { Name = "Thriller" },
                Programming = new Genre() { Name= "Programming" }
            ) ;
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book()
                {
                    Author = "Adams", Title="How to code in c#", Price=19.99F, Content="Ipsum lorem dolor", Genres = new List<Genre> { Programming }
                },
                new Book()
                {
                    Author = "Bella",
                    Title = "How to code in Obectif-c",
                    Price = 39.99F,
                    Content = "Ipsum lorem dolor",
                    Genres = new List<Genre> { Classic }
                },
                new Book()
                {
                    Author = "carole",
                    Title = "How to code in Java",
                    Price = 38.99F,
                    Content = "Ipsum lorem dolor",
                    Genres = new List<Genre> { Romance }
                },
                new Book()
                {
                    Author = "damien",
                    Title = "How to code in phyton",
                    Price = 45.99F,
                    Content = "Ipsum lorem dolor",
                    Genres = new List<Genre> { Thriller }
                }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}