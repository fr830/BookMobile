using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using BookClient.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using AutoMapper;

namespace BookClient.ViewModels
{
    /// <summary>
    /// The view model implements properties and commands to which the view can bind to, 
    /// and notifies the view of any state changes through change notification events.
    /// The properties and commands that the view model provides define the functionality 
    /// to be offered by the UI, but the view determines how that functionality is to be displayed.
    /// Keep the UI responsive with asynchronous operations.
    /// Use asynchronous methods for I/O operations and raise events to asynchronously 
    /// notify views of property changes.
    /// </summary>
    // Model-View-ViewModel (MVVM) Explained
    // https://www.wintellect.com/model-view-viewmodel-mvvm-explained/
    // Getting Started with Xamarin.Forms: Layout Options
    // https://code.tutsplus.com/tutorials/getting-started-with-xamarinforms-layout-options--cms-21644
    // [C# Design Patterns] MVVM (Model-View-ViewModel)
    // https://scottlilly.com/c-design-patterns-mvvm-model-view-viewmodel/
    // Simple MVVM Example
    // https://rachel53461.wordpress.com/2011/05/08/simplemvvmexample/
    // The Model-View-ViewModel Pattern
    // https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm
    // Patterns - WPF Apps With The Model-View-ViewModel Design Pattern
    // https://msdn.microsoft.com/en-us/magazine/dd419663.aspx
    // Win Application Framework (WAF)
    // https://github.com/jbe2277/waf
    public class BookViewModel : ObservableObject
    {
        #region Backing Fields
        
        private Book _book;

        #endregion  // Backing Fields

        #region Constructors

        public BookViewModel()
        {
            _book = new Book();
        }

        public BookViewModel(Book book)
        {
            if (book == null) { throw new ArgumentNullException(nameof(book)); }
            _book = book;
            // Map entity model to view model
            //Mapper.Map<Book, BookViewModel>(book, this);
        }

        #endregion  // Constructors

        #region Properties

        public Book Book
        {
            get
            {
                // Map view model to entity model
                //var book = Mapper.Map<Book>(this);
                return _book;
            }
            set
            {
                SetProperty(ref _book, value);
            }
        }

        #endregion  // Properties
    }
}
