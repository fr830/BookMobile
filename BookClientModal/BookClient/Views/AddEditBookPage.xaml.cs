using AutoMapper;
using BookClient.Extensions;
using BookClient.Models;
using BookClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookClient.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEditBookPage : ContentPage
	{
        readonly BookViewModel _viewModel;
        readonly IList<Book> _books;
        readonly BookManager _manager;

        public AddEditBookPage()
		{
			InitializeComponent();
            this.Title = "AddEditBookPage";
		}

        public AddEditBookPage(BookManager manager, IList<Book> books, BookViewModel viewModel)
        {
            InitializeComponent();

            _manager = manager;
            _books = books;
            _viewModel = viewModel;

            if (viewModel == null)
            {
                _viewModel = new BookViewModel();
                viewModel = _viewModel;
            }

            tableRoot.Title = viewModel.Book.ISBN.IsNullOrEmpty() ? "New Book" : "Edit Book";
            isbnLabel.Text = viewModel.Book.ISBN.IsNullOrEmpty() ? "Will be generated" : viewModel.Book.ISBN;
            //authorCell.Text = viewModel.Book != null ? viewModel.Book.Author : null;

            this.BindingContext = viewModel;

            saveButton.BackgroundColor = viewModel.Book != null ? Color.Gray : Color.Green;
            saveButton.TextColor = Color.White;
            saveButton.Text = viewModel.Book != null ? "Finished" : "Add Book";
            saveButton.CornerRadius = 0;

            saveButton.Clicked += OnDismiss;
        }

        async void OnDismiss(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

            this.IsBusy = true;

            try
            {
                string title = titleCell.Text;
                string author = authorCell.Text;
                string genre = genreCell.Text;

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(genre))
                {
                    this.IsBusy = false;
                    await this.DisplayAlert("Missing Information",
                        "You must enter values for the Title, Author, and Genre.",
                        "OK");
                }
                else
                {
                    if (_viewModel.Book != null)
                    {
                        _viewModel.Book.Title = title;
                        _viewModel.Book.Genre = genre;
                        _viewModel.Book.Authors[0] = author;

                        // Map view model to entity model
                        //Book book = Mapper.Map<Book>(_viewModel);

                        await _manager.Update(_viewModel.Book);
                        int pos = _books.IndexOf(_viewModel.Book);
                        _books.RemoveAt(pos);
                        _books.Insert(pos, _viewModel.Book);
                    }
                    else
                    {
                        Book book = await _manager.Add(title, author, genre);
                        _books.Add(book);
                    }

                    await Navigation.PopModalAsync();
                }
            }
            finally
            {
                this.IsBusy = false;
                button.IsEnabled = true;
            }
        }
    }
}