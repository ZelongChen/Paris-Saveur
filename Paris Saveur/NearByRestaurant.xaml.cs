using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        Geolocator geolocator;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nearbyRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void LocateMe_Click(object sender, RoutedEventArgs e)
        {
            geolocator = new Geolocator();
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
                    Geoposition geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10));

                    var location = new Geopoint(new BasicGeoposition()
                    {
                        Latitude = geoposition.Coordinate.Latitude,
                        Longitude = geoposition.Coordinate.Longitude
                    });

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
