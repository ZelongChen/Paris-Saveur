using Paris_Saveur.DataBase;
using Paris_Saveur.Tools;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur
{

    public sealed partial class SimpleDisplayPage : Page
    {
        private DatabaseHelper _helper;
        private ObservableCollection<Restaurant> _restaurants;
        private string _displayType;

        public SimpleDisplayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _displayType = e.Parameter.ToString();

            ObservableCollection<RestaurantDB> restaurantsDB = new ObservableCollection<RestaurantDB>();
            _helper = new DatabaseHelper();
            if (_displayType.Equals("history"))
            {
                restaurantsDB = _helper.ReadAllRestaurant();
                this.PageTitle.Text = "历史";
            }
            else
            {
                restaurantsDB = _helper.ReadFavoriteRestaurant() ;
                this.PageTitle.Text = "收藏";
            }
            _restaurants = new ObservableCollection<Restaurant>();
            foreach (RestaurantDB restaurantDB in restaurantsDB)
            {
                Restaurant restaurant = new Restaurant();
                restaurant.SetupRestaurantFromDB(restaurantDB);
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
                _restaurants.Add(restaurant);
            }
            this.SimpleDisplayRestaurantList.ItemsSource = _restaurants;
        }

        private void SimpleDisplayRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_displayType == "history")
            {
                _helper.DeleteAllRestaurants();
            }
            else
            {
                _helper.DeleteAllBookmarks();
            }
            _restaurants.Clear();
        }
    }
}
