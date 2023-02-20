using System.ComponentModel;
using WPF.Reader.Model;

namespace WPF.Reader.ViewModel
{
    class ReadBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Title { get; set; }
        public string Description { get; set; }

        public ReadBook(Book book) {

            Title = book.Title;
            Description = book.Content;
        }

        // A vous de jouer maintenant
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    class InDesignReadBook : ReadBook
    {
        public InDesignReadBook() : base(new Book() ) 
        {
        }
    }
}
