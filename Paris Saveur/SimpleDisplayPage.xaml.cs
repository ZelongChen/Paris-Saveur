using Paris_Saveur.DataBase;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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

    public sealed partial class SimpleDisplayPage : Page
    {
        public SimpleDisplayPage()
        {
            this.InitializeComponent();
        }
        DatabaseHelper helper;
        ObservableCollection<Restaurant> restaurants;
        string displayType;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            displayType = e.Parameter.ToString();

            ObservableCollection<RestaurantDB> restaurantsDB = new ObservableCollection<RestaurantDB>();
            helper = new DatabaseHelper();
            if (displayType.Equals("history"))
            {
                restaurantsDB = helper.ReadAllRestaurant();
                this.PageTitle.Text = "历史";
            }
            else
            {
                restaurantsDB = helper.ReadFavoriteRestaurant() ;
                this.PageTitle.Text = "收藏";
            }
            restaurants = new ObservableCollection<Restaurant>();
            foreach (RestaurantDB restaurantDB in restaurantsDB)
            {
                Restaurant restaurant = new Restaurant();
                restaurant.SetupRestaurantFromDB(restaurantDB);
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
                restaurants.Add(restaurant);
            }
            this.SimpleDisplayRestaurantList.ItemsSource = restaurants;
        }

        private void SimpleDisplayRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (displayType == "history")
            {
                helper.DeleteAllRestaurants();
            }
            else
            {
                helper.DeleteAllBookmarks();
            }
            restaurants.Clear();
        }
    }
}
