using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BookClient.Models;
using BookClient.ViewModels;
using Xamarin.Forms;

namespace BookClient.Views
{
    public partial class BookListPage : ContentPage
    {
        //readonly IList<Book> books = new ObservableCollection<Book>();
        //readonly BookManager manager = new BookManager();
        private BookListViewModel _viewModel;

        public BookListPage()
        {
            BindingContext = DependencyService.Get<BookListViewModel>();
            _viewModel = DependencyService.Get<BookListViewModel>();
            InitializeComponent();
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new AddEditBookPage());
        }

        //async void OnRefresh(object sender, EventArgs e)
        //{
        //    // Turn on network indicator
        //    this.IsBusy = true;

        //    try
        //    {
        //        var bookCollection = await manager.GetAll();

        //        foreach (Book book in bookCollection)
        //        {
        //            if (books.All(b => b.ISBN != book.ISBN))
        //                books.Add(book);
        //        }
        //    }
        //    finally
        //    {
        //        // Turn off network indicator
        //        this.IsBusy = false;
        //    }
        //}

        //async void OnAddNewBook(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new AddEditBookPage(manager, books, new BookViewModel()));
        //}

        //async void OnEditBook(object sender, ItemTappedEventArgs e)
        //{
        //    Book book = e.Item as Book;
        //    if (book == null) return;
        //    await Navigation.PushModalAsync(new AddEditBookPage(manager, books, new BookViewModel(book)));
        //}

        //async void OnDeleteBook(object sender, EventArgs e)
        //{
        //    MenuItem item = (MenuItem)sender;
        //    Book book = item.CommandParameter as Book;

        //    if (book != null)
        //    {
        //        if (await this.DisplayAlert("Delete Book?",
        //            "Are you sure you want to delete the book '"
        //                + book.Title + "'?", "Yes", "Cancel") == true)
        //        {
        //            this.IsBusy = true;
        //            try
        //            {
        //                await manager.Delete(book.ISBN);
        //                books.Remove(book);
        //            }
        //            finally
        //            {
        //                this.IsBusy = false;
        //            }

        //        }
        //    }
        //}
    }
}
