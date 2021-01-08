using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using BookClient.Droid;
using BookClient.Extensions;
using BookClient.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(MainPage), typeof(ExtendedTabbedPageRenderer))]
namespace BookClient.Droid
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        public ExtendedTabbedPageRenderer(Context context) : base(context) { }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            base.DispatchDraw(canvas);
            SetTabIcons();
        }

        private void SetTabIcons()
        {

            var element = this.Element;
            if (element == null)
            {
                return;
            }
            
            Activity activity = this.Context as Activity;
            if (null != activity && null != activity.ActionBar && activity.ActionBar.TabCount > 0)
            {

                for (int i = 0; i < element.Children.Count; i += 1)
                {
                    Android.App.ActionBar.Tab tab = activity.ActionBar.GetTabAt(i);

                    var page = element.Children[i];
                    if ((null != tab) && (null != page) && (null != page.IconImageSource))
                    {
                        var contentPage = page as ContentPage;
                        if (contentPage != null)
                        {
                            //Typeface font = Typeface.CreateFromAsset(Context.Assets, "fa-solid-900.ttf");
                            // TextDrawable icon = new TextDrawable.Builder().BeginConfig().TextColor(Android.Graphics.Color.Red).UseFont(font)
                            //     .FontSize(30).EndConfig()
                            //     .BuildRect("\uf15c", Android.Graphics.Color.Red);
                            // var icon = Context.GetDrawable(Resource.Drawable.Calculator);
                            var icon = new IconDrawable(this.Context, "\uf15c", "fa-solid-900")
                                .Color(Xamarin.Forms.Color.Blue.ToAndroid())
                                .SizeDp(24);
                            switch (i)
                            {
                                case 0:
                                    // icon = Context.GetDrawable(Resource.Drawable.Calculator);
                                    icon = new IconDrawable(this.Context, "\uf1ec", "fa-solid-900");
                                    break;
                                case 1:
                                    icon = new IconDrawable(this.Context, "\uf02e", "fa-solid-900");
                                    break;
                                case 2:
                                    icon = new IconDrawable(this.Context, "\uf70c", "fa-solid-900");
                                    break;
                                case 3:
                                    icon = new IconDrawable(this.Context, "\uf044", "fa-solid-900");
                                    break;
                                case 4:
                                    icon = new IconDrawable(this.Context, "\uf03a", "fa-solid-900");
                                    break;
                            }
                            icon.Color(Xamarin.Forms.Color.Blue.ToAndroid())
                                .SizeDp(24);
                            tab.SetIcon(icon);
                            tab.SetText(string.Empty);
                        }
                    }
                }
            }
        }
    }
}