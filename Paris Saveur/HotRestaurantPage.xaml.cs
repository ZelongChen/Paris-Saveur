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

        int currentPage = 1;
        string sortBy = "popularity";
        string restaurantStyle;
        Tag tag;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameterReceived = e.Parameter;
            if (parameterReceived == null)
            {
                DownloadRecommendedRestaurant(sortBy, currentPage);
            }
            else if (parameterReceived is string)
            {
                restaurantStyle = parameterReceived as string;
                DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage);
            } 
            else
            {
                tag = new Tag();
                tag = parameterReceived as Tag;
                DownloadRestaurantWithTag(tag.name, sortBy, currentPage);

            }
        }

        private async void DownloadRestaurantWithTag(string tag, string sortby, int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list-by-tag/?tag_name=" + tag + "&order=-" + sortBy + "&page=" + page);
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
            this.hotRestaurantList.DataContext = list;
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
            this.hotRestaurantList.DataContext = list;
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
            this.hotRestaurantList.DataContext = list;
        }

        private void hotRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void RefreshPage(string sortBy, int page)
        {
            if (restaurantStyle == null && tag == null)
            {
                DownloadRecommendedRestaurant(sortBy, currentPage);
            }
            else if (restaurantStyle == null && tag != null)
            {
                DownloadRestaurantWithTag(tag.name, sortBy, page);
            }
            else
            {
                DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage);
            }
        }

        private void SortByPopularity_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "popularity";
            RefreshPage(sortBy, currentPage);

            
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "rating_score";
            RefreshPage(sortBy, currentPage);
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            sortBy = "rating_num";
            RefreshPage(sortBy, currentPage);
        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRecommendedRestaurant(sortBy, currentPage++);
        }
    }
}
