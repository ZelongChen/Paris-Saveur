using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            RefreshPage();
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
                restaurant.SetupRestaurantModelToDisplay(this.BaseUri);
            }
            this.RecommendedRestaurantList.DataContext = list;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        private void RecommendedRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void RefreshPage()
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.RecommendedRestaurantList.Visibility = Visibility.Visible;
                DownloadRecommendedRestaurant();
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.RecommendedRestaurantList.Visibility = Visibility.Collapsed;
            }
        }
    }
}
