using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Net.Http;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Paris_Saveur
{
    public sealed partial class NearByRestaurant : Page
    {
        private Geoposition _geoposition;
        private RestaurantList _list;

        public NearByRestaurant()
        {
            this.InitializeComponent();
            _list = new RestaurantList();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.NearbyRestaurantList.Visibility = Visibility.Visible;
                if (e.Parameter == null)
                {
                    this.AppBar.Visibility = Visibility.Visible;
                    FindCurrentLocationAnRestaurantsNearby();
                }
                else
                {
                    this.AppBar.Visibility = Visibility.Collapsed;
                    var station = e.Parameter as TransportStation;
                    FindRestauransAroundStation(station);
                }
                
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.NearbyRestaurantList.Visibility = Visibility.Collapsed;
            }
        }

        private void NearbyRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private async void OpenMap_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"bingmaps:?cp=" + _geoposition.Coordinate.Point.Position.Latitude + "~" + _geoposition.Coordinate.Point.Position.Longitude + "&lvl=16";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.NearbyRestaurantList.Visibility = Visibility.Visible;
                this.ScrollViewer.ChangeView(null, 0d, null);
                FindCurrentLocationAnRestaurantsNearby();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.NearbyRestaurantList.Visibility = Visibility.Collapsed;
            }
        }

        private async void DownloadNearByRestaurant(string latitude, string longitude)
        {
            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/_list-by-location/?geo_lat=" + latitude + "&geo_lon=" + longitude + "&criterion=geopoint&order=-popularity&page=1";
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            _list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            if (_list.Restaurant_list.Count == 0)
            {
                LoadingRing.IsActive = false;
                LoadingRing.Visibility = Visibility.Collapsed;
                this.RefreshButton.IsEnabled = true;
                this.LaunchMapButton.IsEnabled = true;
                this.NoRestaurantText.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                this.NoRestaurantText.Visibility = Visibility.Collapsed;
            }
            foreach (Restaurant restaurant in _list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.NearbyRestaurantList.DataContext = _list;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
            this.RefreshButton.IsEnabled = true;
            this.LaunchMapButton.IsEnabled = true;
        }

        private void FindRestauransAroundStation(TransportStation station)
        {
            DownloadNearByRestaurant(station.Latitude, station.Longitude);
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
                    _geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10));
                    DownloadNearByRestaurant("" + _geoposition.Coordinate.Point.Position.Latitude, "" + _geoposition.Coordinate.Point.Position.Longitude);
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

    }
}
