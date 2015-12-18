using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System.Net.Http;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur
{

    public sealed partial class HotTagPage : Page
    {
        public HotTagPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.TagList.Visibility = Visibility.Visible;
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                DownloadHotTag();
            }
            else
            {
                this.TagList.Visibility = Visibility.Collapsed;
                this.NoConnectionText.Visibility = Visibility.Visible;
            }
            
        }

        private async void DownloadHotTag()
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/tag-cloud/");
            var result = await response.Content.ReadAsStringAsync();

            TagList list = Newtonsoft.Json.JsonConvert.DeserializeObject<TagList>(result);
            foreach (Tag tag in list.tag_cloud)
            {
                tag.tagToString = tag.name + " (" + tag.num_tagged + ")";
            }
            this.TagList.ItemsSource = list.tag_cloud;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void TagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tag tag = e.AddedItems[0] as Tag;
            Frame.Navigate(typeof(HotRestaurantPage), tag);
        }

    }
}
