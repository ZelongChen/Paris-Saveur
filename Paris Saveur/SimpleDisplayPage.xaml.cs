using Paris_Saveur.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace Paris_Saveur
{

    public sealed partial class SimpleDisplayPage : Page
    {
        public SimpleDisplayPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ObservableCollection<RestaurantDB> restaurants = new ObservableCollection<RestaurantDB>();
            DatabaseHelper helper = new DatabaseHelper();
            restaurants = helper.ReadAllRestaurant();
            this.SimpleDisplayRestaurantList.ItemsSource = restaurants;
        }

        private void SimpleDisplayRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RestaurantDB restaurant = e.AddedItems[0] as RestaurantDB;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }
    }
}
