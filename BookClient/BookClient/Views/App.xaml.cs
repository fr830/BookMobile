using AutoMapper;
using BookClient.Models;
using BookClient.ViewModels;
using BookClient.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BookClient
{
    public partial class App : Application
	{
        public static Settings Settings { get; set; }

		public App()
		{
            App.Settings = SettingsLoader.ImprovedLoad();

            DependencyService.Register<BookListViewModel>();

            // Load resources
            InitializeComponent();

            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperConfig()));

            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
    }
}
