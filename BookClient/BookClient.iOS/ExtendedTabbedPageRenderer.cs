using System;
using System.Threading.Tasks;
using BookClient.iOS;
using BookClient.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainPage), typeof(ExtendedTabbedPageRenderer))]
namespace BookClient.iOS
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        UITabBarController tabbedController;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        
            if (e.NewElement != null)
            {
                tabbedController = (UITabBarController)ViewController;
            }
        }
        
        // protected override Task<Tuple<UIImage, UIImage>> GetIcon(Page page)
        // {
        //     UIImage image;
        //     
        //     if (page.Title == "App")
        //     {
        //         image = UIImage.FromBundle(page.Icon.File).ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
        //     }
        //     else
        //     {
        //         image = UIImage.FromBundle(page.Icon.File).ImageWithRenderingMode(UIImageRenderingMode.Automatic);
        //     }
        //
        //     return Task.FromResult(new Tuple<UIImage, UIImage>(image, image));
        // }
    }
}