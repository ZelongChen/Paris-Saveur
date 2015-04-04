using Paris_Saveur.DataBase;
using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Calls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Text;
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

    public sealed partial class RestaurantDetailPage : Page
    {
        public RestaurantDetailPage()
        {
            this.InitializeComponent();
        }

        Restaurant restaurant;
        private List<String> SocialNetworks { get; set; }
        RestaurantDB restaurantDB;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += RestaurantDetailPage_DataRequested;
            restaurant = e.Parameter as Restaurant;
            this.PageTitle.Text = restaurant.name;
            this.CommentPivotItemHeader.Text = "评论" + " (" + restaurant.rating_num + ")";
            SetupRestaurantDetail(restaurant);

            restaurantDB = new RestaurantDB();
            restaurantDB.SetupRestaurantDB(restaurant);
            restaurantDB.Bookmarked = false;
            restaurantDB.ViewTime = new DateTime().ToString();

            DatabaseHelper DbHelper = new DatabaseHelper();
            DbHelper.Insert(restaurantDB);
        }

        /*private async void SaveThumbnail()
        {
            StorageFolder appFolder = await KnownFolders.PicturesLibrary.CreateFolderAsync("ParisSaveurImageFolder", CreationCollisionOption.OpenIfExists);
            StorageFile file = await appFolder.CreateFileAsync(restaurant.name + ".jpg", CreationCollisionOption.ReplaceExisting);
            restaurantDB.thumbnail = file.Path.ToString();

            IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
            WriteableBitmap image = restaurant.ThumbnailWriteableBitmap;
            Stream pixelStream = image.PixelBuffer.AsStream();
            byte[] pixels = new byte[image.PixelBuffer.Length];
            await pixelStream.ReadAsync(pixels, 0, pixels.Length);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)image.PixelWidth, (uint)image.PixelHeight, 96.0, 96.0, pixels);
            await encoder.FlushAsync();

        }*/


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += RestaurantDetailPage_DataRequested;
        }

        private void SetupRestaurantDetail(Restaurant restaurant)
        {
            this.restaurantThumbnail.Source = restaurant.ThumbnailBitmap;
            this.restaurantStyle.Text = restaurant.style;
            this.restaurantPrice.Text = restaurant.consumption_per_capita;
            SetupRestaurantReview(restaurant);
            if (restaurant.description != "")
            {
                this.restaurantDescription.Text = restaurant.description;
            }
            else
            {
                this.restaurantDescription.Visibility = Visibility.Collapsed;
            }
            this.restaurantAddress.Text = restaurant.address;
            this.restaurantMetro.Text = restaurant.public_transit;
            this.restaurantTime.Text = restaurant.opening_hours;
            this.restaurantPhoneNumber1.Text = restaurant.phone_number_1;
            if (restaurant.phone_number_2 != "")
            {
                this.restaurantPhoneNumber2.Text = restaurant.phone_number_2;
                this.restaurantPhoneNumber2.Visibility = Visibility.Visible;
            }
            else
            {
                this.restaurantPhoneNumber2.Visibility = Visibility.Collapsed;
            }
        }

        private void SetupRestaurantReview(Restaurant restaurant)
        {
            BitmapImage halfStar = new BitmapImage();
            halfStar.UriSource = new Uri(this.star1.BaseUri, "Assets/star_half.png");
            BitmapImage emptyStar = new BitmapImage();
            emptyStar.UriSource = new Uri(this.star1.BaseUri, "Assets/star_empty.png");
            BitmapImage star = new BitmapImage();
            star.UriSource = new Uri(this.star1.BaseUri, "Assets/star_full.png");
            double ratingScore = Double.Parse(restaurant.rating_score);
            
            if (ratingScore == 0)
            {
                this.star1.Source = emptyStar;
                this.star2.Source = emptyStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 0 && ratingScore <1)
            {
                this.star1.Source = halfStar;
                this.star2.Source = emptyStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 1)
            {
                this.star1.Source = star;
                this.star2.Source = emptyStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore >1 && ratingScore < 2)
            {
                this.star1.Source = star;
                this.star2.Source = halfStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 2)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 2 && ratingScore < 3)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = halfStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 3)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 3 && ratingScore < 4)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = halfStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 4)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = star;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 4 && ratingScore < 5)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = star;
                this.star5.Source = halfStar;
            }
            if (ratingScore == 5)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = star;
                this.star5.Source = star;
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.RestaurantDetailPivot.SelectedIndex == 1)
            {
                if (comments.Comments.Count == 0)
                {
                    DownloadRestaurantCommentsAtPage(1);
                }
                this.CommentPivotItemHeader.Foreground = new SolidColorBrush(Color.FromArgb(100, 224, 92, 82));
                this.CommentPivotItemHeader.FontWeight = FontWeights.Bold;
                this.CommentPivotItemHeader.FontSize = 22;
                this.DetailPivotItemHeader.Foreground = new SolidColorBrush(Colors.Black);
                this.DetailPivotItemHeader.FontWeight = FontWeights.Normal;
                this.DetailPivotItemHeader.FontSize = 20;
            }
            else
            {
                this.DetailPivotItemHeader.Foreground = new SolidColorBrush(Color.FromArgb(100, 224, 92, 82));
                this.DetailPivotItemHeader.FontWeight = FontWeights.Bold;
                this.DetailPivotItemHeader.FontSize = 22;
                this.CommentPivotItemHeader.Foreground = new SolidColorBrush(Colors.Black);
                this.CommentPivotItemHeader.FontWeight = FontWeights.Normal;
                this.CommentPivotItemHeader.FontSize = 20;
                SetupRestaurantDetail(restaurant);
            }
        }

        int currentPage = 1;
        CommentList comments = new CommentList();

        private async void DownloadRestaurantCommentsAtPage(int page)
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;
            loadMoreButoon.Visibility = Visibility.Collapsed;

            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/restaurants/json/rating-list/" + restaurant.pk + "/?page=" + page;
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
            if (restaurantComment.rating_list.Count == 0 && currentPage == 1)
            {
                this.NoCommentText.Visibility = Visibility.Visible;
            }
            else
            {
                this.NoCommentText.Visibility = Visibility.Collapsed;
            }
            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comment.convertDateToChinese();
                comment.username = comment.user.username;
            }

            foreach (LatestRating comment in restaurantComment.rating_list)
            {
                comments.Comments.Add(comment);
                ImageDownloader.DownloadImageIntoImage(comment.user);
            }
            restaurantCommentList.DataContext = comments;
        }
        private void loadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRestaurantCommentsAtPage(++currentPage);
        }

        void RestaurantDetailPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var deferral = request.GetDeferral();

            request.Data.Properties.Title = "我发现了一家好吃的餐馆";
            request.Data.SetText("\n" + restaurant.name + "\n" + restaurant.address);

            deferral.Complete();
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void restaurantPhoneNumber1_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallManager.ShowPhoneCallUI(restaurant.phone_number_1, restaurant.name);
        }

        private void restaurantPhoneNumber2_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallManager.ShowPhoneCallUI(restaurant.phone_number_2, restaurant.name);
        }
    }
}
