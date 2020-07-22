using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BookClient.Models;

namespace BookClient.ViewModels
{
    public class BookListViewModel : INotifyPropertyChanged
    {
        BookManager _repository;
        IList<Book> _books = new ObservableCollection<Book>();
        int _currentBook;

        public event PropertyChangedEventHandler PropertyChanged;

        public BookListViewModel()
        {
            _repository = new BookManager();

            var bookCollection = _repository.GetBooks();

            foreach (Book book in bookCollection)
            {
                if (_books.All(b => b.ISBN != book.ISBN))
                    _books.Add(book);
            }
        }

        public IList<Book> Books => _books;

        public Book CurrentBook
        {
            get
            {
                return Books[_currentBook];
            }

            set
            {
                int index = Books.IndexOf(value);
                if (index >= 0)
                {
                    _currentBook = index;
                    RaisePropertyChanged(nameof(CurrentBook));
                }
            }
        }

        /// <summary>
        /// Raise the PropertyChanged notification.
        /// </summary>
        /// <param name="propertyName"></param>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
