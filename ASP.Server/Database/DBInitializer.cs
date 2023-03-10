using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;
using ASP.Server.Data;
using System.Xml.Linq;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller, Programming, Mystery,
                Biography, Physics, Philosophy, SelfHelp, Entrepreneurship,
                Psychology, Productivity, Business, Fantasy, Science;
            Author Austen, Agatha, Orwell, Rowling, Sparks, Shelley, Flynn,
                Wells, Bronte, Lee, Adams, Paquet, Edison, Einstein, Hawking,
                Nietzsche, Peterson, Kahneman, Clear, Gladwell;
            bookDbContext.Authors.AddRange(
                Austen = new Author() { Name = "Jane Austen" },
                Agatha = new Author() { Name = "Agatha Christie"},
                Orwell = new Author() { Name = "George Orwell" },
                Rowling = new Author { Name = "J.K. Rowling" },
                Sparks = new Author { Name = "Nicholas Sparks" },
                Shelley = new Author { Name = "Mary Shelley" },
                Flynn = new Author { Name = "Gillian Flynn" },
                Wells = new Author { Name = "H.G. Wells" },
                Bronte = new Author { Name = "Emily Bronte" },
                Lee = new Author { Name = "Harper Lee" },
                Adams = new Author { Name = "Scott Adams" },
                Paquet = new Author { Name = "Judicael Paquet" },
                Edison = new Author { Name = "Thomas Edison" },
                Einstein = new Author { Name = "Albert Einstein" },
                Hawking = new Author { Name = "Stephen Hawking" },
                Nietzsche = new Author { Name = "Friedrich Nietzsche" },
                Peterson = new Author { Name = "Jordan B. Peterson" },
                Kahneman = new Author { Name = "Daniel Kahneman" },
                Clear = new Author { Name = "James Clear" },
                Gladwell = new Author { Name = "Malcolm Gladwell" }
            );

            bookDbContext.Genre.AddRange(
                SF = new Genre() { Name = "Science Fiction" },
                Classic = new Genre() { Name = "Classic" },
                Romance = new Genre() { Name = "Romance" },
                Thriller = new Genre() { Name = "Thriller" },
                Programming = new Genre { Name = "Programming" },
                Mystery = new Genre { Name = "Mystery" },
                Biography = new Genre { Name = "Biography" },
                Physics = new Genre { Name = "Physics" },
                Philosophy = new Genre { Name = "Philosophy" },
                SelfHelp = new Genre { Name = "Self-Help" },
                Entrepreneurship = new Genre { Name = "Entrepreneurship" },
                Psychology = new Genre { Name = "Psychology" },
                Productivity = new Genre { Name = "Productivity" },
                Business = new Genre { Name = "Business" },
                Science = new Genre { Name = "Science"},
                Fantasy = new Genre {  Name = "Fantasy"}
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }

            bookDbContext.Books.AddRange(
                new Book() { Author = Austen, Title = "Pride and Prejudice", Price = 9.99,
                    Content = "It is a truth universally acknowledged, that a single man in possession of a good fortune, must be in want of a wife.",
                    Genres = new() { Romance, Classic } },
                new Book() { Author = Agatha, Title = "Murder on the Orient Express", Price = 12.99,
                    Content = "The impossible could not have happened, therefore the impossible must be possible in spite of appearances.",
                    Genres = new() { Thriller } },
                new Book() { Author = Orwell, Title = "1984", Price = 14.99,
                    Content = "War is peace. Freedom is slavery. Ignorance is strength.",
                    Genres = new() { SF, Classic } },
                new Book() { Author = Rowling, Title = "Harry Potter and the Philosopher's Stone", Price = 11.99,
                    Content = "It does not do to dwell on dreams and forget to live.",
                    Genres = new() { Fantasy } },
                new Book() { Author = Sparks, Title = "The Notebook", Price = 10.99,
                    Content = "The best love is the kind that awakens the soul and makes us reach for more, that plants a fire in our hearts and brings peace to our minds.",
                    Genres = new() { Romance } },
                new Book() { Author = Shelley, Title = "Frankenstein", Price = 8.99,
                    Content = "Beware; for I am fearless, and therefore powerful.",
                    Genres = new() { SF, Classic } },
                new Book() { Author = Flynn, Title = "Gone Girl", Price = 13.99,
                    Content = "I'm a big fan of the lie of omission.",
                    Genres = new() { Thriller } },
                new Book() { Author = Wells, Title = "The War of the Worlds", Price = 11.99,
                    Content = "No one would have believed in the last years of the nineteenth century that this world was being watched keenly and closely by intelligences greater than man's and yet as mortal as his own;",
                    Genres = new() { SF } },
                new Book() { Author = Bronte, Title = "Wuthering Heights", Price = 8.99,
                    Content = "Whatever our souls are made of, his and mine are the same.",
                    Genres = new() { Classic, Romance } },
                new Book() { Author = Lee, Title = "To Kill a Mockingbird", Price = 14.99,
                    Content = "Shoot all the bluejays you want, if you can hit 'em, but remember it's a sin to kill a mockingbird.",
                    Genres = new() { Classic } },
                new Book() { Author = Adams, Title = "Best practices with C#", Price = 19.99,
                    Content = "The best way to predict the future is to invent it.",
                    Genres = new() { Programming, Business } },
                new Book() { Author = Paquet, Title = "How to run a successful blog", Price = 12.99,
                    Content = "Blogging is not a business, it's a platform for your ideas.",
                    Genres = new() { SelfHelp, Entrepreneurship } },
                new Book() { Author = Edison, Title = "Inventing the light bulb", Price = 7.99,
                    Content = "I have not failed. I've just found 10,000 ways that won't work.",
                    Genres = new() { Biography } },
                new Book() { Author = Agatha, Title = "Murder on the Orient Express", Price = 9.99,
                    Content = "The impossible could not have happened, therefore the impossible must be possible in spite of appearances.",
                    Genres = new() { Mystery } },
                new Book() { Author = Einstein, Title = "The Theory of Relativity", Price = 19.99,
                    Content = "The most beautiful thing we can experience is the mysterious. It is the source of all true art and science.",
                    Genres = new() { Science, Physics } },
                new Book() { Author = Hawking, Title = "A Brief History of Time", Price = 12.99,
                    Content = "We are just an advanced breed of monkeys on a minor planet of a very average star. But we can understand the Universe. That makes us something very special.",
                    Genres = new() { Science, Physics } },
                new Book() { Author = Nietzsche, Title = "Thus Spoke Zarathustra", Price = 7.99,
                    Content = "He who has a why to live can bear almost any how.",
                    Genres = new() { Philosophy } },
                new Book() { Author = Hawking, Title = "The Universe in a Nutshell", Price = 9.99,
                    Content = "The universe doesn't allow perfection.",
                    Genres = new() { Science, Physics } },
                new Book() { Author = Einstein, Title = "Relativity: The Special and the General Theory", Price = 19.99,
                    Content = "Reality is merely an illusion, albeit a very persistent one.",
                    Genres = new() { Science, Physics } },
                new Book() { Author = Peterson, Title = "12 Rules for Life: An Antidote to Chaos", Price = 12.99,
                    Content = "Compare yourself to who you were yesterday, not to who someone else is today.",
                    Genres = new() { SelfHelp, Philosophy } },
                new Book() { Author = Kahneman, Title = "Thinking, Fast and Slow", Price = 7.99,
                    Content = "The mind is a machine for jumping to conclusions.",
                    Genres = new() { Psychology } },
                new Book() { Author = Clear, Title = "Atomic Habits", Price = 9.99,
                    Content = "You do not rise to the level of your goals. You fall to the level of your systems.",
                    Genres = new() { SelfHelp, Productivity } },
                new Book() { Author = Gladwell, Title = "Outliers: The Story of Success", Price = 14.99,
                    Content = "The biggest misconception about success is that we do it solely on our smarts, ambition, hustle and hard work.",
                    Genres = new() { Psychology, Business } }
            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}