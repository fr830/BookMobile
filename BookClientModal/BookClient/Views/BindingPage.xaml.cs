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
	public partial class BindingPage : ContentPage
	{
		public BindingPage ()
		{
			InitializeComponent();
            finishButton.Clicked += async (s, e) => await Navigation.PopModalAsync();
        }
    }
}