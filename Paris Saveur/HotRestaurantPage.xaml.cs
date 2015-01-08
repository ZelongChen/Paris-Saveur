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

    public sealed partial class HotRestaurantPage : Page
    {
        public HotRestaurantPage()
        {
            this.InitializeComponent();
        }

        int currentPage = 1;
        string sortBy = "popularity";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DownloadRecommendedRestaurant(sortBy, currentPage);
        }

        private async void DownloadRecommendedRestaurant(string sortby, int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list/?order=-" + sortBy +"&page=" + page);
            var result = await response.Content.ReadAsStringAsync();

            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            foreach (Restaurant restaurant in list.restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
            }
            this.hotRestaurantList.ItemsSource = list.restaurant_list;
        }

        private void hotRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void SortByPopularity_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "popularity";
            DownloadRecommendedRestaurant(sortBy, currentPage);
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "rating_score";
            DownloadRecommendedRestaurant(sortBy, currentPage);
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "rating_num";
            DownloadRecommendedRestaurant(sortBy, currentPage);
        }
    }
}
