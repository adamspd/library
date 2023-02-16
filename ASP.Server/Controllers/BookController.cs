﻿using Microsoft.AspNetCore.Mvc;
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
        [Display(Name = "titre")]
        public String Title { get; set; }

        [Required]
        [Display(Name = "contenu")]
        public String Content { get; set; }

        [Required]
        [Display(Name = "prix")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "auteur")]
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

        public ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = libraryDbContext.Books.ToList();
            return View(ListBooks);
        }

        public ActionResult<List<Book>> DeleteBook(int id)
        {
            Console.WriteLine("ca marche");
            Book book= libraryDbContext.Books.FirstOrDefault(x => x.Id == id);
            libraryDbContext.Books.Remove(book);
            libraryDbContext.SaveChanges();

            return View("List", libraryDbContext.Books.ToList());
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
                libraryDbContext.Add(new Book() { Genres = genre, Content = book.Content , Price = book.Price, Title = book.Title, Author = book.Author});
                libraryDbContext.SaveChanges();
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = libraryDbContext.Genre.ToList() });
        }
    }
}
