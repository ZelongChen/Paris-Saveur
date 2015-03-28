using Paris_Saveur.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
        int currentPage = 1;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FindCurrentLocationAnRestaurantsNearby();
        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nearbyRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            FindCurrentLocationAnRestaurantsNearby();

        }

        private async void DownloadNearByRestaurant(Geoposition currentPosition, int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/list-by-location/?geo_lat=" + currentPosition.Coordinate.Point.Position.Latitude + "&geo_lon=" + currentPosition.Coordinate.Point.Position.Longitude + "&criterion=geopoint&order=-popularity&page=" + currentPage;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
            }
            this.nearbyRestaurantList.ItemsSource = list.Restaurant_list;
        }

        private async void FindCurrentLocationAnRestaurantsNearby()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            if (geolocator.LocationStatus == PositionStatus.Disabled)
            {
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
                    DownloadNearByRestaurant(geoposition, currentPage);
                }
                catch (UnauthorizedAccessException)
                {
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

    }
}
