using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        CommentList comments = new CommentList();
        List<LatestRating> ratings = new List<LatestRating>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                this.NoConnectionText.Visibility = Visibility.Collapsed;
                this.restaurantCommentList.Visibility = Visibility.Visible;
                restaurantPk = (int)e.Parameter;
                DownloadRestaurantCommentsAtPage(currentPage++);
            }
            else
            {
                this.NoConnectionText.Visibility = Visibility.Visible;
                this.restaurantCommentList.Visibility = Visibility.Collapsed;
                this.loadMoreButoon.Visibility = Visibility.Collapsed;
            }
        }

        private async void DownloadRestaurantCommentsAtPage(int page)
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;
            loadMoreButoon.Visibility = Visibility.Collapsed;

            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/rating-list/" + restaurantPk + "/?page=" + page;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
            loadMoreButoon.Visibility = Visibility.Visible;
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
            {
                return;
            }

            RestaurantComment restaurantComment = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantComment>(result);
            if (restaurantComment.rating_list.Count < 12)
            {
                loadMoreButoon.Visibility = Visibility.Collapsed;
            }
            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comment.convertDateToChinese();
                comment.username = comment.user.username;
            }
            ratings.AddRange(restaurantComment.rating_list);

            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comments.Comments.Add(comment);
                ImageDownloader.DownloadImageIntoImage(comment.user);
            }
            restaurantCommentList.DataContext = comments;

        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.checkNetworkConnection())
            {
                DownloadRestaurantCommentsAtPage(currentPage++);
                this.loadMoreButoon.Content = "加载更多";
                this.loadMoreButoon.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                this.loadMoreButoon.Content = "请检查您的网络连接";
                this.loadMoreButoon.Foreground = new SolidColorBrush(Colors.Gray);
            }
           
        }
    }
}
