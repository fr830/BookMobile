using AutoMapper;
using BookClient.Models;
using BookClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookClient.Views
{
    public partial class MainPage : ContentPage
    {
        readonly IList<Book> books = new ObservableCollection<Book>();
        readonly BookManager manager = new BookManager();

        public MainPage()
        {
            BindingContext = books;
            InitializeComponent();
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            // Turn on network indicator
            this.IsBusy = true;

            try {
                var bookCollection = await manager.GetAll();

                foreach (Book book in bookCollection)
                {
                    if (books.All(b => b.ISBN != book.ISBN))
                        books.Add(book);
                }
            }
            finally {
                // Turn off network indicator
                this.IsBusy = false;
            }
        }

        async void OnBehavior(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BehaviorPage());
        }

        async void OnBinding(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BindingPage());
        }

        async void OnFluent(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FluentValidationPage());
        }

        async void OnBookList(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BookListPage());
        }

        async void OnSquareRoot(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SquareRootPage());
        }

        async void OnAddNewBook(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddEditBookPage(manager, books, new BookViewModel()));
        }

        async void OnEditBook(object sender, ItemTappedEventArgs e)
        {
            Book book = e.Item as Book;
            if (book == null) return;
            await Navigation.PushModalAsync(new AddEditBookPage(manager, books, new BookViewModel(book)));
        }

        async void OnDeleteBook(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Book book = item.CommandParameter as Book;

            if (book != null)
            {
                if (await this.DisplayAlert("Delete Book?",
                    "Are you sure you want to delete the book '"
                        + book.Title + "'?", "Yes", "Cancel") == true)
                {
                    this.IsBusy = true;
                    try {
                        await manager.Delete(book.ISBN);
                        books.Remove(book);
                    }
                    finally {
                        this.IsBusy = false;
                    }

                }
            }
        }
    }
}
