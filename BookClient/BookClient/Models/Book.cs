using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BookClient.Models
{
    // The model represents the domain model which includes a data model along with business and validation logic.
    [DebuggerDisplay("Book: {Title} {Genre}")]
    public class Book : INotifyPropertyChanged
    {
        private string _isbn;
        private string _title;
        private string _genre;
        private DateTime _publishDate;
        private ObservableCollection<string> _authors;

        public event PropertyChangedEventHandler PropertyChanged;

        public Book()
        {
            //Id = new Guid.NewGuid();
            _authors = new ObservableCollection<string>();
        }

        //public Guid Id {get; private set; }

        public string ISBN
        {
            get { return _isbn; }
            set { ChangePropertyValue(ref _isbn, value); }
        }

        //[Required(ErrorMessageResourceName = nameof(Resources.TitleMandatory), ErrorMessageResourceType = typeof(Resources))]
        //[StringLength(30, ErrorMessageResourceName = nameof(Resources.TitleMandatory), ErrorMessageResourceType = typeof(Resources))]
        public string Title
        {
            get { return _title; }
            set { ChangePropertyValue(ref _title, value); }
        }

        public string Genre
        {
            get { return _genre; }
            set { ChangePropertyValue(ref _genre, value); }
        }

        public ObservableCollection<string> Authors
        {
            get { return _authors; }
            set { ChangePropertyValue(ref _authors, value); }
        }

        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { ChangePropertyValue(ref _publishDate, value); }
        }

        public string Author
        {
            get { return _authors.FirstOrDefault(); }
            set
            {
                if (_authors.Count == 0)
                    _authors.Add(value);
                if (_authors[0] != value)
                {
                    _authors[0] = value;
                    RaisePropertyChanged("Author");
                }
            }
        }

        public override bool Equals(object obj)
        {
            var book = obj as Book;

            if (book == null || this.GetType() != obj.GetType())
                return false;

            return book.ISBN == this.ISBN;
        }

        public override int GetHashCode()
        {
            return _isbn.GetHashCode();
        }

        /// <summary>
        /// Helper method to test a field value against a new value,
        /// do the assignment if they are different, and then raise the
        /// property change notification.
        /// </summary>
        /// <typeparam name="T">Type being changed</typeparam>
        /// <param name="field">Field</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>True if the value was changed.</returns>
        private bool ChangePropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Raises the INotifyPropertyChanged event.
        /// </summary>
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Title;
        }
    }
}

