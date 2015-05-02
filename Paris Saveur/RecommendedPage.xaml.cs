using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur
{
    public sealed partial class RecommendedPage : Page
    {
        public RecommendedPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.recommendedRestaurantList.Visibility = Visibility.Visible;
                DownloadRecommendedRestaurant();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.recommendedRestaurantList.Visibility = Visibility.Collapsed;
            }
        }

        private async void DownloadRecommendedRestaurant()
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;
            

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/recommended/?order=-popularity&page=1");
            var result = await response.Content.ReadAsStringAsync();

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.recommendedRestaurantList.DataContext = list;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection()) {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.recommendedRestaurantList.Visibility = Visibility.Visible;
                DownloadRecommendedRestaurant();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.recommendedRestaurantList.Visibility = Visibility.Collapsed;
            }

        }

        private void recommendedRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }
    }
}
