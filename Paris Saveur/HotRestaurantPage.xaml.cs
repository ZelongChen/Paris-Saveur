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
        string restaurantStyle;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            restaurantStyle = e.Parameter as string;
            if (restaurantStyle == null)
            {
                DownloadRecommendedRestaurant(sortBy, currentPage);
            }
            else
            {
                DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage);
            }
        }

        private async void DownloadRestaurantWithStyle(string style, string sortby, int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list-by-style/" + restaurantStyle + "/?order=-" + sortBy + "&page=" + page);
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
            if (restaurantStyle == null)
            {
                DownloadRecommendedRestaurant(sortBy, currentPage);
            }
            else
            {
                DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage);
            }
            
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "rating_score";
            if (restaurantStyle == null)
            {
                DownloadRecommendedRestaurant(sortBy, currentPage);
            }
            else
            {
                DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage);
            }
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "rating_num";
            if (restaurantStyle == null)
            {
                DownloadRecommendedRestaurant(sortBy, currentPage);
            }
            else
            {
                DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage);
            }
        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
