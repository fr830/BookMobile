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
	public partial class FluentValidationPage : ContentPage
	{
        SignUpPageViewModel _viewModel;

        public FluentValidationPage ()
		{
			InitializeComponent ();
            _viewModel = new SignUpPageViewModel();
            BindingContext = _viewModel;
            finishButton.Clicked += async (s, e) => await Navigation.PopModalAsync();
        }
    }
}