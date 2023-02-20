﻿using System.Collections.ObjectModel;
using WPF.Reader.Api;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        /*    public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>() {
                new Book(),
                new Book(),
                new Book()
            };*/

        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !

        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 



        public ObservableCollection<Book> Books{ get; set; } = new ObservableCollection<Book>();
        public LibraryService() {

            var listapi = new BookApi().BookGetBooks();

            foreach (var book in listapi)
            {
                Books.Add(book);
            }
        }
    }
}
