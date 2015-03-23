using Paris_Saveur.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
            DownloadRecommendedRestaurantAtPage(currentPage++);          
        }

        private int currentPage = 1;
        private RestaurantList restaurantList = new RestaurantList();

        private async void DownloadRecommendedRestaurantAtPage(int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/recommended/?order=-popularity&page=" + this.currentPage);
            var result = await response.Content.ReadAsStringAsync();

            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                restaurantList.Restaurant_list.Add(restaurant);
            }
            this.recommendedRestaurantList.DataContext = restaurantList;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRecommendedRestaurantAtPage(currentPage++);
        }

        private void recommendedRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRecommendedRestaurantAtPage(currentPage++);
        }
    }
}
