using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Book SelectedBook;

        public Book SelectedLine { get { return SelectedBook; } set { 
                if (value != null)
                {
                    var book = value as Book;
                    SelectedBook = BookById(book.Id);
                    Ioc.Default.GetService<INavigationService>().Navigate<DetailsBook>(SelectedBook);
                }

            } 
        }

        public ICommand ItemSelectedCommand { get; set; }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<Book> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;
        public Book BookById(int id)
        {
            return Ioc.Default.GetRequiredService<LibraryService>().BookById(id);

        }

        

        public ListBook()
        {
            ItemSelectedCommand = new RelayCommand(book => { /* the livre devrais etre dans la variable book */ });
        }
    }
}
