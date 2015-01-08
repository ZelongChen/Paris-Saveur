﻿using Paris_Saveur.Model;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DownloadRecommendedRestaurant();
        }

        private async void DownloadRecommendedRestaurant()
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list/?order=-popularity&page=1");
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRecommendedRestaurant();
        }

        private void hotRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }
    }
}
