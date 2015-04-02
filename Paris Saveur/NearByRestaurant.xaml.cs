﻿using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class NearByRestaurant : Page
    {
        public NearByRestaurant()
        {
            this.InitializeComponent();
        }

        Geoposition geoposition;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.nearbyRestaurantList.Visibility = Visibility.Visible;
                FindCurrentLocationAnRestaurantsNearby();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.nearbyRestaurantList.Visibility = Visibility.Collapsed;
            }
        }

        private void nearbyRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private async void DownloadNearByRestaurant(Geoposition currentPosition)
        {
            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/list-by-location/?geo_lat=" + currentPosition.Coordinate.Point.Position.Latitude + "&geo_lon=" + currentPosition.Coordinate.Point.Position.Longitude + "&criterion=geopoint&order=-popularity&page=1";
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            if (list.Restaurant_list.Count == 0)
            {
                LoadingRing.IsActive = false;
                LoadingRing.Visibility = Visibility.Collapsed;
                this.RefreshButton.IsEnabled = true;
                this.LaunchMapButton.IsEnabled = true;
                return;
            }
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.nearbyRestaurantList.DataContext = list;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
            this.RefreshButton.IsEnabled = true;
            this.LaunchMapButton.IsEnabled = true;
        }

        private async void FindCurrentLocationAnRestaurantsNearby()
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;
            this.RefreshButton.IsEnabled = false;
            this.LaunchMapButton.IsEnabled = false;

            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            if (geolocator.LocationStatus == PositionStatus.Disabled)
            {
                LoadingRing.IsActive = false;
                LoadingRing.Visibility = Visibility.Collapsed;
                this.RefreshButton.IsEnabled = true;
                this.LaunchMapButton.IsEnabled = true;
                MessageBox("定位功能未打开");
            }
            else
            {
                try
                {
                    // Getting Current Location  
                    geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10));
                    DownloadNearByRestaurant(geoposition);
                }
                catch (UnauthorizedAccessException)
                {
                    LoadingRing.IsActive = false;
                    LoadingRing.Visibility = Visibility.Collapsed;
                    this.RefreshButton.IsEnabled = true;
                    this.LaunchMapButton.IsEnabled = true;
                    MessageBox("定位功能未打开");
                }
            }
        }

        // Custom Message Dialog Box  
        private async void MessageBox(string message)
        {
            var dialog = new MessageDialog(message.ToString());
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => await dialog.ShowAsync());
        }

        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.nearbyRestaurantList.Visibility = Visibility.Visible;
                this.ScrollViewer.ChangeView(null, 0d, null);
                FindCurrentLocationAnRestaurantsNearby();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.nearbyRestaurantList.Visibility = Visibility.Collapsed;
            }
        }
    }
}
