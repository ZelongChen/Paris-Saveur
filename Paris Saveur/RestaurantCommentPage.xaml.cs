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
            restaurantPk = (int) e.Parameter;
            DownloadRestaurantCommentsAtPage(currentPage++);
        }

        private async void DownloadRestaurantCommentsAtPage(int page)
        {
            LoadingBar.IsEnabled = true;
            LoadingBar.Visibility = Visibility.Visible;
            loadMoreButoon.Visibility = Visibility.Collapsed;

            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/rating-list/" + restaurantPk + "/?page=" + page;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            LoadingBar.IsEnabled = false;
            LoadingBar.Visibility = Visibility.Collapsed;
            loadMoreButoon.Visibility = Visibility.Visible;
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
            {
                return;
            }

            RestaurantComment restaurantComment = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantComment>(result);
            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comment.convertDateToChinese();
                comment.username = comment.user.username;
            }
            ratings.AddRange(restaurantComment.rating_list);

            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comments.Comments.Add(comment);
                Task downloadThumbnail = ImageDownloader.DownloadImageIntoImage(comment.user);
                await downloadThumbnail;
            }
            restaurantCommentList.DataContext = comments;

        }

        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRestaurantCommentsAtPage(currentPage++);
        }
    }
}
