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
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.StartScreen;
using Windows.UI.Notifications;
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
        DatabaseHelper helper;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DataTransferManager.GetForCurrentView().DataRequested += RestaurantDetailPage_DataRequested;
            restaurant = e.Parameter as Restaurant;
            this.PageTitle.Text = restaurant.name;
            this.CommentPivotItemHeader.Text = "评论" + " (" + restaurant.rating_num + ")";

            helper = new DatabaseHelper();
            restaurantDB = helper.ReadRestaurant(restaurant.pk);
            if (restaurantDB == null)
            {
                restaurantDB = new RestaurantDB();
                restaurantDB.SetupRestaurantDB(restaurant);
                restaurantDB.Bookmarked = false;
            }
            restaurantDB.ViewTime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            helper.Insert(restaurantDB);

            SetupRestaurantDetail(restaurant);
        }

        private void InitAppBar()
        {
            ToggleAppBarButton(!SecondaryTile.Exists("" + restaurant.pk));
        }

        private void ToggleAppBarButton(bool showPinButton)
        {
            if (showPinButton)
            {
                this.PinUnPinCommandButton.Label = "添加到桌面";
            }
            else
            {
                this.PinUnPinCommandButton.Label = "从桌面删除";
            }

            this.PinUnPinCommandButton.UpdateLayout();
        }

        async void pinToAppBar_Click(object sender, RoutedEventArgs e)
        {

            if (SecondaryTile.Exists("" + restaurant.pk))
            {

                SecondaryTile secondaryTile = new SecondaryTile("" + restaurant.pk);

                await secondaryTile.RequestDeleteForSelectionAsync(MainPage.GetElementRect((FrameworkElement)sender), Windows.UI.Popups.Placement.Above);
                ToggleAppBarButton(true);
            }
            else
            {
                Uri square150x150Logo = new Uri("ms-appx:///Assets/logo_transparent.png");

                string tileActivationArguments = MainPage.appbarTileId + " WasPinnedAt=" + DateTime.Now.ToLocalTime().ToString();

                SecondaryTile secondaryTile = new SecondaryTile("" + restaurant.pk,
                                                                restaurant.name,
                                                                tileActivationArguments,
                                                                square150x150Logo,
                                                                TileSize.Square150x150);
                secondaryTile.VisualElements.ForegroundText = ForegroundText.Dark;

                secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;

                await secondaryTile.RequestCreateAsync();
                ToggleAppBarButton(false);
            }
        }

        void BottomAppBar_Opened(object sender, object e)
        {
            ToggleAppBarButton(!SecondaryTile.Exists(MainPage.appbarTileId));
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
            CheckBookmark();
            InitAppBar();
            if (restaurant.ThumbnailBitmap != null)
            {
                this.restaurantThumbnail.Source = restaurant.ThumbnailBitmap;
            }
            else
            {
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/restaurant_thumbnail_placeholder.jpg"));
                restaurant.ThumbnailBitmap = placeholder;
                if (ConnectionContext.CheckNetworkConnection())
                {
                    ImageDownloader.DownloadImageIntoImage(this.restaurantThumbnail, restaurant);
                }
            }
            SetupRestaurantReview(restaurant);
            if (restaurant.description == "")
            {
                this.restaurantDescription.Visibility = Visibility.Collapsed;
            }
            this.RestaurantInfoPivot.DataContext = restaurant;

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
                this.CommentPivotItemHeader.Foreground = new SolidColorBrush(Color.FromArgb(255, 224, 92, 82));
                this.CommentPivotItemHeader.FontWeight = FontWeights.Bold;
                this.CommentPivotItemHeader.FontSize = 22;
                this.DetailPivotItemHeader.Foreground = new SolidColorBrush(Colors.Black);
                this.DetailPivotItemHeader.FontWeight = FontWeights.Normal;
                this.DetailPivotItemHeader.FontSize = 20;
            }
            else
            {
                this.DetailPivotItemHeader.Foreground = new SolidColorBrush(Color.FromArgb(255, 224, 92, 82));
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

            request.Data.Properties.Title = "";
            request.Data.SetText("\n" + restaurant.name + "\n" + restaurant.address + "\n" + restaurant.public_transit + "\n" + restaurant.phone_number_1 + "\n" + ("来自 @巴黎吃什么"));

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

        private void AddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            if (restaurantDB.Bookmarked == false)
            {
                restaurantDB.Bookmarked = true;
                helper.UpdateRestaurant(restaurantDB);
                CheckBookmark();
                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
                XmlNodeList elements = toastXml.GetElementsByTagName("text");
                elements[0].AppendChild(toastXml.CreateTextNode(restaurantDB.name + " 已添加到收藏"));
                ToastNotification toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
            else
            {
                restaurantDB.Bookmarked = false;
                helper.UpdateRestaurant(restaurantDB);
                CheckBookmark();
                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
                XmlNodeList elements = toastXml.GetElementsByTagName("text");
                elements[0].AppendChild(toastXml.CreateTextNode("已从收藏删除 " + restaurantDB.name));
                ToastNotification toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }

        }

        private void CheckBookmark()
        {
            if (restaurantDB.Bookmarked == true)
            {
                this.FavoriteButton.Icon = new SymbolIcon(Symbol.Accept);
                this.FavoriteButton.Label = "已收藏";
            }
            else
            {
                this.FavoriteButton.Icon = new SymbolIcon(Symbol.Add);
                this.FavoriteButton.Label = "收藏";
            }
        }

        private async void OpenMapButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"bingmaps:?collection=point." + restaurant.geo_lat +"_" + restaurant.geo_lon + "_" + restaurant.name +"&lvl=16";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string authToken = (String)localSettings.Values["AuthToken"];
            if (authToken != null)
            {
                Frame.Navigate(typeof(CommentPage), restaurant.pk);
            }
            else
            {
                Frame.Navigate(typeof(LoginPage), "ComeFromDetailPage");
            }
            
        }

        private async void restaurantAddressGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"bingmaps:?collection=point." + restaurant.geo_lat + "_" + restaurant.geo_lon + "_" + restaurant.name + "&lvl=16";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void restaurantTimeGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var appointment = new Windows.ApplicationModel.Appointments.Appointment();
            appointment.Subject = "去 " + restaurant.name + " 吃饭";
            appointment.Location = restaurant.name;
            appointment.Reminder = TimeSpan.FromHours(1);
            var rect = MainPage.GetElementRect(sender as FrameworkElement);

            String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
                                   appointment, rect, Windows.UI.Popups.Placement.Default);
        }

        private async void ShareLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
            msg.Body = restaurant.name + "\n" + restaurant.address + "\n" + restaurant.public_transit;
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(msg);
        }
    }
}
