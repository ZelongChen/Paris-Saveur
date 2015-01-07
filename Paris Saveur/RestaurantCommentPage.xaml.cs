using Paris_Saveur.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

    public sealed partial class RestaurantCommentPage : Page
    {
        public RestaurantCommentPage()
        {
            this.InitializeComponent();
        }

        int restaurantPk;
        int currentPage = 1;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            restaurantPk = (int) e.Parameter;
            DownloadRestaurantCommentsAtPage(currentPage);
        }

        private async void DownloadRestaurantCommentsAtPage(int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;

            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/rating-list/" + restaurantPk + "/?page=" + page;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;

            RestaurantComment restaurantComment = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantComment>(result);
            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comment.convertDateToChinese();
                comment.username = comment.user.username;
            }

            this.restaurantCommentList.ItemsSource = restaurantComment.rating_list;
            
        }
    }
}
