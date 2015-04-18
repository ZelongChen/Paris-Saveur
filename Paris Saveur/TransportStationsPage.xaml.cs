using Paris_Saveur.Model;
using System;
using System.Collections.Generic;
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


namespace Paris_Saveur
{
    public sealed partial class TransportStationsPage : Page
    {
        public TransportStationsPage()
        {
            this.InitializeComponent();
            TransportStationList list = new TransportStationList();
            this.DataContext = list;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void TransportStationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var station = e.AddedItems[0] as TransportStation;
            if (Frame != null)
            {
                Frame.Navigate(typeof(NearByRestaurant), station);
            }
        }
    }
}
