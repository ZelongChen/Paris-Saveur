﻿using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace Paris_Saveur
{

    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Focus(Windows.UI.Xaml.FocusState.Programmatic);
        }

        private void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (ConnectionContext.checkNetworkConnection())
                {
                    this.NoConnectionText.Visibility = Visibility.Collapsed;
                    this.SearchResultList.Visibility = Visibility.Visible;
                    DownloadSearchedRestaurant();
                }
                else
                {
                    this.NoConnectionText.Visibility = Visibility.Visible;
                    this.SearchResultList.Visibility = Visibility.Collapsed;
                }
                InputPane.GetForCurrentView().TryHide();
            }
        }

        private async void DownloadSearchedRestaurant()
        {
            this.NoResultText.Visibility = Visibility.Collapsed;
            this.SearchResultList.Visibility = Visibility.Visible;

            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list-search/?keyword=" + SearchTextBox.Text + "&order=-popularity&page=1");
            var result = await response.Content.ReadAsStringAsync();

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            if (list.Restaurant_list.Count == 0)
            {
                this.NoResultText.Visibility = Visibility.Visible;
                this.SearchResultList.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (Restaurant restaurant in list.Restaurant_list)
                {
                    restaurant.ConvertRestaurantStyleToChinese();
                    restaurant.ShowReviewScoreAndNumber();
                    restaurant.ShowPrice();
                    BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                    restaurant.ThumbnailBitmap = placeholder;
                    ImageDownloader.DownloadImageIntoImage(restaurant);
                }
                this.SearchResultList.DataContext = list;
            }

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void SearchResultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InputPane.GetForCurrentView().TryShow();
        }
    }
}