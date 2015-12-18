using Paris_Saveur.Model;
using Windows.UI.Xaml.Controls;
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
