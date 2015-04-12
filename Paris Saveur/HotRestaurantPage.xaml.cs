using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

    public sealed partial class HotRestaurantPage : Page
    {
        public HotRestaurantPage()
        {
            this.InitializeComponent();
        }

        int currentPage = 1;
        string sortBy = "popularity";
        string restaurantStyle;
        Tag restaurantTag;
        RestaurantList restaurantList = new RestaurantList();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.hotRestaurantList.Visibility = Visibility.Visible;

                var parameterReceived = e.Parameter;
                if (parameterReceived == null)
                {
                    this.Title.Text = "热门餐馆";
                    DownloadRecommendedRestaurant(sortBy, currentPage++);
                }
                else if (parameterReceived is string)
                {
                    restaurantStyle = parameterReceived as string;
                    this.Title.Text = StyleToChinese(restaurantStyle);
                    DownloadRestaurantWithStyle(restaurantStyle, sortBy, currentPage++);
                }
                else
                {
                    restaurantTag = new Tag();
                    restaurantTag = parameterReceived as Tag;
                    this.Title.Text = restaurantTag.name;
                    DownloadRestaurantWithTag(restaurantTag.name, sortBy, currentPage++);
                }
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.hotRestaurantList.Visibility = Visibility.Collapsed;
                this.loadMoreButoon.Visibility = Visibility.Collapsed;
            }
        }

        private async void DownloadRestaurantWithTag(string tag, string sortby, int page)
        {
            loadMoreButoon.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list-by-tag/?tag_name=" + tag + "&order=-" + sortBy + "&page=" + page);
            var result = await response.Content.ReadAsStringAsync();

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            if (list.Restaurant_list.Count < 12)
            {
                loadMoreButoon.Visibility = Visibility.Collapsed;
            }
            else
            {
                loadMoreButoon.Visibility = Visibility.Visible;
            }
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                restaurantList.Restaurant_list.Add(restaurant);
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.hotRestaurantList.DataContext = restaurantList;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private async void DownloadRestaurantWithStyle(string style, string sortby, int page)
        {
            loadMoreButoon.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list-by-style/" + restaurantStyle + "/?order=-" + sortBy + "&page=" + page);
            var result = await response.Content.ReadAsStringAsync();

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            if (list.Restaurant_list.Count < 12)
            {
                loadMoreButoon.Visibility = Visibility.Collapsed;
            }
            else
            {
                loadMoreButoon.Visibility = Visibility.Visible;
            }
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                restaurantList.Restaurant_list.Add(restaurant);
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.hotRestaurantList.DataContext = restaurantList;

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private async void DownloadRecommendedRestaurant(string sortby, int page)
        {
            loadMoreButoon.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var response = await client.GetAsync("http://www.vivelevendredi.com/restaurants/json/list/?order=-" + sortBy +"&page=" + page);
            var result = await response.Content.ReadAsStringAsync();

            RestaurantList list = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantList>(result);
            if (list.Restaurant_list.Count < 12)
            {
                loadMoreButoon.Visibility = Visibility.Collapsed;
            }
            else
            {
                loadMoreButoon.Visibility = Visibility.Visible;
            }
            foreach (Restaurant restaurant in list.Restaurant_list)
            {
                restaurant.ConvertRestaurantStyleToChinese();
                restaurant.ShowReviewScoreAndNumber();
                restaurant.ShowPrice();
                restaurantList.Restaurant_list.Add(restaurant);
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                ImageDownloader.DownloadImageIntoImage(restaurant);
            }
            this.hotRestaurantList.DataContext = restaurantList;
    
            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
        }

        private void hotRestaurantList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Restaurant restaurant = e.AddedItems[0] as Restaurant;
            Frame.Navigate(typeof(RestaurantDetailPage), restaurant);
        }

        private void RefreshPage(string sortBy, int page)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                this.hotRestaurantList.Visibility = Visibility.Visible;
                this.NoConnectionText.Visibility = Visibility.Collapsed;

                if (restaurantStyle == null && restaurantTag == null)
                {
                    DownloadRecommendedRestaurant(sortBy, page);
                }
                else if (restaurantStyle == null && restaurantTag != null)
                {
                    DownloadRestaurantWithTag(restaurantTag.name, sortBy, page);
                }
                else
                {
                    DownloadRestaurantWithStyle(restaurantStyle, sortBy, page);
                }
            }
            else
            {
                currentPage--;
                this.hotRestaurantList.Visibility = Visibility.Collapsed;
                this.NoConnectionText.Visibility = Visibility.Visible;
            }
        }

        private void SortByPopularity_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            sortBy = "popularity";
            restaurantList = new RestaurantList();
            RefreshPage(sortBy, currentPage++);            
        }

        private void SortByRatingScore_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            sortBy = "rating_score";
            restaurantList = new RestaurantList();
            RefreshPage(sortBy, currentPage++);
        }

        private void SortByRatingNum_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            sortBy = "rating_num";
            restaurantList = new RestaurantList();
            RefreshPage(sortBy, currentPage++);
        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                RefreshPage(sortBy, currentPage++);
                this.loadMoreButoon.Content = "加载更多";
                this.loadMoreButoon.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                this.loadMoreButoon.Content = "请检查您的网络连接";
                this.loadMoreButoon.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        public string StyleToChinese(string style)
        {
            switch (style)
            {
                case "Sichuan_Hunan":
                    return "川菜 湘菜";
                case "Shandong_Anhui":
                    return "鲁菜 徽菜";
                case "Jiangsu_Zhejiang":
                    return "苏菜 浙菜";
                case "Cantonese_Fujian":
                    return "粤菜 闽菜";
                case "Yunnan":
                    return "云南菜";
                case "Northern_Chinese":
                    return "北方菜系";
                case "Japanese_Korean":
                    return "日餐 韩餐";
                case "South_Asian":
                    return "东南亚菜";
                default:
                    return "未归类";
            }
        }
    }
}
