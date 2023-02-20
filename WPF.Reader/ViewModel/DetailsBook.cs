using CommunityToolkit.Mvvm.DependencyInjection;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; }
        public Book CurrentBook { get; init; }

        public DetailsBook(Book book)
        {
            CurrentBook = book;
            ReadCommand = new RelayCommand(x => {

                Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(CurrentBook);
            });

        }
    }

    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() ) { }
    }
}

