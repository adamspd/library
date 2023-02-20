﻿using CommunityToolkit.Mvvm.DependencyInjection;
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



        public Book SelectedBook;

        public Book SelectedLine
        {
            get { return SelectedBook; }
            set
            {
                if (value != null)
                {
                    SelectedBook = value;
                    Ioc.Default.GetService<INavigationService>().Navigate<ReadBook>(SelectedBook);
                }

            }
        }

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public Book CurrentBook { get; init; }

        public DetailsBook(Book book)
        {
            CurrentBook = book;
            ReadCommand = new RelayCommand(x => {

                /* A vous de définir la commande */
                Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(CurrentBook);
            });

        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new Book() /*{ Title = "Test Book" }*/) { }
    }
}

