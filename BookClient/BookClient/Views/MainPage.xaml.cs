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
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.Children.Add(new SquareRootPage());
            this.Children.Add(new BindingPage());
            this.Children.Add(new BehaviorPage());
            this.Children.Add(new FluentValidationPage());
            this.Children.Add(new BookListPage());
            //this.Children.Add(new BookListSimplePage());
        }
        
        //async void OnSquareRoot(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new SquareRootPage());
        //}

        //async void OnBinding(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new BindingPage());
        //}

        //async void OnBehavior(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new BehaviorPage());
        //}

        //async void OnFluent(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new FluentValidationPage());
        //}

        //async void OnBookListSimple(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new BookListSimplePage());
        //}

        //async void OnBookList(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new BookListPage());
        //}

    }
}
