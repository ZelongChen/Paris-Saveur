using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Net.Http;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur
{

    public sealed partial class HotRestaurantPage : Page
    {
        private int _currentPage;
        private string _sortBy;
        private string _restaurantStyle;
        private Tag _restaurantTag;
        private RestaurantList _restaurantList;
        private string _baseURL;
        private enum LISTTYPE
        {
            Recommended,
            Tag,
            Style
        }

        public HotRestaurantPage()
        {
            this.InitializeComponent();
            _currentPage = 1;
            _sortBy = "popularity";
            _restaurantList = new RestaurantList();
            _baseURL = "http://www.vivelevendredi.com/restaurants/json";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.HotRestaurantList.Visibility = Visibility.Visible;

                var parameterReceived = e.Parameter;
                if (parameterReceived == null)
                {
                    this.Title.Text = "热门餐馆";
                    DownloadRestaurants((int)LISTTYPE.Recommended, "", _sortBy, _currentPage++);
                }
                else if (parameterReceived is string)
                {
                    _restaurantStyle = parameterReceived as string;
                    this.Title.Text = Restaurant.StyleToChinese(_restaurantStyle);
                    DownloadRestaurants((int)LISTTYPE.Style, _restaurantStyle, _sortBy, _currentPage++);
                }
                else
                {
                    _restaurantTag = new Tag();
                    _restaurantTag = parameterReceived as Tag;
                    this.Title.Text = _restaurantTag.name;
                    DownloadRestaurants((int)LISTTYPE.Tag, _restaurantTag.name, _sortBy, _currentPage++);
                }
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.HotRestaurantList.Visibility = Visibility.Collapsed;
                this.LoadMoreButoon.Visibility = Visibility.Collapsed;
            }
        }

        private void HotRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void SortByPopularity_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            _sortBy = "popularity";
            _restaurantList = new RestaurantList();
            RefreshPage(_sortBy, _currentPage++);            
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            _sortBy = "rating_score";
            _restaurantList = new RestaurantList();
            RefreshPage(_sortBy, _currentPage++);
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            _sortBy = "rating_num";
            _restaurantList = new RestaurantList();
            RefreshPage(_sortBy, _currentPage++);
        }

        private void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                RefreshPage(_sortBy, _currentPage++);
                this.LoadMoreButoon.Content = "加载更多";
                this.LoadMoreButoon.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                this.LoadMoreButoon.Content = "请检查您的网络连接";
                this.LoadMoreButoon.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private async void DownloadRestaurants(int type, string keyword, string sortby, int page)
        {
            LoadMoreButoon.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var client = new HttpClient();
            RestaurantList list = new RestaurantList();

            switch (type)
            {
                case (int)LISTTYPE.Recommended:
                    var responseRecommended = await client.GetAsync(_baseURL + "/list/?order=-" + _sortBy + "&page=" + page);
                    var resultRecommended = await responseRecommended.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(resultRecommended);
                    break;
                case (int)LISTTYPE.Style:
                    var responseStyle = await client.GetAsync(_baseURL + "/list-by-style/" + keyword + "/?order=-" + _sortBy + "&page=" + page);
                    var resultStyle = await responseStyle.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(resultStyle);
                    break;
                default:
                    var responseTag = await client.GetAsync(_baseURL + "/list-by-tag/?tag_name=" + keyword + "&order=-" + _sortBy + "&page=" + page);
                    var resultTag = await responseTag.Content.ReadAsStringAsync();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(resultTag);
                    break;
            }

            if (list.Restaurant_list.Count < 12)
            {
                LoadMoreButoon.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadMoreButoon.Visibility = Visibility.Visible;
            }
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                _restaurantList.Restaurant_list.Add(restaurant);
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.HotRestaurantList.DataContext = _restaurantList;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void RefreshPage(string _sortBy, int page)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                this.HotRestaurantList.Visibility = Visibility.Visible;
                this.NoConnectionText.Visibility = Visibility.Collapsed;

                if (_restaurantStyle == null && _restaurantTag == null)
                {
                    DownloadRestaurants((int)LISTTYPE.Recommended, "", _sortBy, page);
                }
                else if (_restaurantStyle == null && _restaurantTag != null)
                {
                    DownloadRestaurants((int)LISTTYPE.Recommended, _restaurantTag.name, _sortBy, page);
                }
                else
                {
                    DownloadRestaurants((int)LISTTYPE.Recommended, _restaurantStyle, _sortBy, page);
                }
            }
            else
            {
                _currentPage--;
                this.HotRestaurantList.Visibility = Visibility.Collapsed;
                this.NoConnectionText.Visibility = Visibility.Visible;
            }
        }
    }
}
