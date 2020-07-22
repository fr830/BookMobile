using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace BookClient.Models
{
    // The model represents the domain model which includes
    // a data model along with business and validation logic.
    [DebuggerDisplay("Book: {Title} {Genre}")]
    public class Book : ObservableObject
    {
        private string _isbn;
        private string _title;
        private string _genre;
        private DateTime _publishDate;
        private ObservableCollection<string> _authors;

        public Book()
        {
            //Id = new Guid.NewGuid();
            _authors = new ObservableCollection<string>();
        }

        //public Guid Id {get; private set; }

        public string ISBN
        {
            get { return _isbn; }
            set { SetProperty(ref _isbn, value); }
        }

        //[Required(ErrorMessageResourceName = nameof(Resources.TitleMandatory), ErrorMessageResourceType = typeof(Resources))]
        //[StringLength(30, ErrorMessageResourceName = nameof(Resources.TitleMandatory), ErrorMessageResourceType = typeof(Resources))]
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }

        public ObservableCollection<string> Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { SetProperty(ref _publishDate, value); }
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
                    OnPropertyChanged("Author");
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
    }
}

