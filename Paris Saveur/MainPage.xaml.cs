using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace Paris_Saveur
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            ImageDownloader.DownloadImageIntoImage(recommendedImage, "https://pbs.twimg.com/profile_images/1550954462/instagramIcon_400x400.png");
            ImageDownloader.DownloadImageIntoImage(nearbyImage, "http://cdn.marketplaceimages.windowsphone.com/v8/images/b821d889-4d99-42dc-b711-0f93c63e192a?imageType=ws_icon_large");
            ImageDownloader.DownloadImageIntoImage(hotImage, "http://static1.squarespace.com/static/50dcd3bfe4b0395512971edc/t/54346f07e4b0ebb220d2199b/1412722441352/Instagram.jpg");
            ImageDownloader.DownloadImageIntoImage(sortbyImage, "http://i2.cdn.turner.com/money/dam/assets/141219073832-bieber-instagram-620xa.jpg");
            ImageDownloader.DownloadImageIntoImage(hottagImage, "http://cdns2.freepik.com/free-photo/_408-64283.jpg");
            ImageDownloader.DownloadImageIntoImage(metroImage, "http://s3-static-ak.buzzfed.com/static/2014-06/30/11/campaign_images/webdr10/now-kylie-jenner-has-been-accused-of-photoshoppin-2-13413-1404140944-12_big.jpg");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void recommendedText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecommendedPage));
        }

        private void hotText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage));
        }

        private void nearbyText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NearByRestaurant));
        }

        private void sortbyText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantSortByStylePage));
        }

        private void hottagText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotTagPage));
        }

        private void metroText_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
