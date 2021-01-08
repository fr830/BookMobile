using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookClient.Views
{
	public partial class BehaviorPage : ContentPage
	{
		public BehaviorPage ()
		{
			InitializeComponent();
			this.IconImageSource = new FontImageSource { FontFamily = "fa-solid", Size = 30,  Glyph= "\uf70c", Color = Color.Blue }; 
			this.Title = string.Empty;
            //finishButton.Clicked += async (s, e) => await Navigation.PopModalAsync();
        }
    }
}