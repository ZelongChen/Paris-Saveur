using Paris_Saveur.DataBase;
using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.ApplicationModel.Calls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.StartScreen;
using Windows.UI.Notifications;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            _currentPage = 1;
            _comments = new CommentList();
        }

        Restaurant _restaurant;
        private List<String> SocialNetworks { get; set; }
        RestaurantDB _restaurantDB;
        DatabaseHelper _helper;
        int _currentPage;
        CommentList _comments;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            DataTransferManager.GetForCurrentView().DataRequested += RestaurantDetailPage_DataRequested;
            _restaurant = e.Parameter as Restaurant;
            this.PageTitle.Text = _restaurant.name;
            this.CommentPivotItemHeader.Text = LocalizedStrings.Get("RestaurantDetailPage_PivotHeaderComment") + " (" + _restaurant.rating_num + ")";

            _helper = new DatabaseHelper();
            _restaurantDB = _helper.ReadRestaurant(_restaurant.pk);
            if (_restaurantDB == null)
            {
                _restaurantDB = new RestaurantDB();
                _restaurantDB.SetupRestaurantDB(_restaurant);
                _restaurantDB.Bookmarked = false;
            }
            _restaurantDB.ViewTime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            _helper.Insert(_restaurantDB);

            SetupRestaurantDetail(_restaurant);
        }

        private void InitAppBar()
        {
            ToggleAppBarButton(!SecondaryTile.Exists("" + _restaurant.pk));
        }

        private void ToggleAppBarButton(bool showPinButton)
        {
            this.PinUnPinCommandButton.Label = showPinButton ? LocalizedStrings.Get("RestaurantDetailPage_PinText") : LocalizedStrings.Get("RestaurantDetailPage_UnPinText");
            this.PinUnPinCommandButton.UpdateLayout();
        }

        async void PinToAppBar_Click(object sender, RoutedEventArgs e)
        {

            if (SecondaryTile.Exists("" + _restaurant.pk))
            {

                SecondaryTile secondaryTile = new SecondaryTile("" + _restaurant.pk);

                await secondaryTile.RequestDeleteForSelectionAsync(MainPage.GetElementRect((FrameworkElement)sender), Windows.UI.Popups.Placement.Above);
                ToggleAppBarButton(true);
            }
            else
            {
                Uri square150x150Logo = new Uri("ms-appx:///Assets/logo_transparent.png");

                string tileActivationArguments = MainPage.APP_BAR_TILE_ID + " WasPinnedAt=" + DateTime.Now.ToLocalTime().ToString();

                SecondaryTile secondaryTile = new SecondaryTile("" + _restaurant.pk,
                                                                _restaurant.name,
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
            ToggleAppBarButton(!SecondaryTile.Exists(MainPage.APP_BAR_TILE_ID));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += RestaurantDetailPage_DataRequested;
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.RestaurantDetailPivot.SelectedIndex == 1)
            {
                if (_comments.Comments.Count == 0)
                {
                    DownloadRestaurantCommentsAtPage(1);
                }
                SetupPivotItemHeader(false, true);
            }
            else
            {
                SetupPivotItemHeader(true, false);
                SetupRestaurantDetail(_restaurant);
            }
        }

        private void LoadMoreButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadRestaurantCommentsAtPage(++_currentPage);
        }

        void RestaurantDetailPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var deferral = request.GetDeferral();

            request.Data.Properties.Title = "";
            request.Data.SetText("\n" + _restaurant.name + "\n" + _restaurant.address + "\n" + _restaurant.public_transit + "\n" + _restaurant.phone_number_1 + "\n" + ("来自 @巴黎吃什么"));

            deferral.Complete();
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void RestaurantPhoneNumber1_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallManager.ShowPhoneCallUI(_restaurant.phone_number_1, _restaurant.name);
        }

        private void RestaurantPhoneNumber2_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallManager.ShowPhoneCallUI(_restaurant.phone_number_2, _restaurant.name);
        }

        private void AddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            _restaurantDB.Bookmarked = !_restaurantDB.Bookmarked;
            _helper.UpdateRestaurant(_restaurantDB);
            CheckBookmark();
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            XmlNodeList elements = toastXml.GetElementsByTagName("text");
            string text = _restaurantDB.Bookmarked ? _restaurantDB.name + LocalizedStrings.Get("RestaurantDetailPage_AddToFavoriteText") : LocalizedStrings.Get("RestaurantDetailPage_RemoveFromFavoriteText") + _restaurantDB.name;
            elements[0].AppendChild(toastXml.CreateTextNode(text));
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

        private async void OpenMapButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"bingmaps:?collection=point." + _restaurant.geo_lat +"_" + _restaurant.geo_lon + "_" + _restaurant.name +"&lvl=16";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string authToken = (String)localSettings.Values["AuthToken"];
            if (authToken != null)
            {
                Frame.Navigate(typeof(CommentPage), _restaurant.pk);
            }
            else
            {
                Frame.Navigate(typeof(LoginPage), "ComeFromDetailPage");
            }
            
        }

        private async void RestaurantAddressGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string uriToLaunch = @"bingmaps:?collection=point." + _restaurant.geo_lat + "_" + _restaurant.geo_lon + "_" + _restaurant.name + "&lvl=16";
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void RestaurantTimeGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var appointment = new Windows.ApplicationModel.Appointments.Appointment();
            appointment.Subject = "去 " + _restaurant.name + " 吃饭";
            appointment.Location = _restaurant.name;
            appointment.Reminder = TimeSpan.FromHours(1);
            var rect = MainPage.GetElementRect(sender as FrameworkElement);

            String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
                                   appointment, rect, Windows.UI.Popups.Placement.Default);
        }

        private async void ShareLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
            msg.Body = _restaurant.name + "\n" + _restaurant.address + "\n" + _restaurant.public_transit;
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(msg);
        }


        private void SetupRestaurantDetail(Restaurant _restaurant)
        {
            CheckBookmark();
            InitAppBar();
            if (_restaurant.ThumbnailBitmap != null)
            {
                this.RestaurantThumbnail.Source = _restaurant.ThumbnailBitmap;
            }
            else
            {
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/_restaurant_thumbnail_placeholder.jpg"));
                _restaurant.ThumbnailBitmap = placeholder;
                if (ConnectionContext.CheckNetworkConnection())
                {
                    ImageDownloader.DownloadImageIntoImage(this.RestaurantThumbnail, _restaurant);
                }
            }
            SetupRestaurantReview(_restaurant);
            if (_restaurant.description == "")
            {
                this.RestaurantDescription.Visibility = Visibility.Collapsed;
            }
            this.RestaurantInfoPivot.DataContext = _restaurant;

            this.RestaurantPhoneNumber1.Text = _restaurant.phone_number_1;
            if (_restaurant.phone_number_2 != "")
            {
                this.RestaurantPhoneNumber2.Text = _restaurant.phone_number_2;
                this.RestaurantPhoneNumber2.Visibility = Visibility.Visible;
            }
            else
            {
                this.RestaurantPhoneNumber2.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckBookmark()
        {
            this.FavoriteButton.Icon = _restaurantDB.Bookmarked ? new SymbolIcon(Symbol.Accept) : new SymbolIcon(Symbol.Add);
            this.FavoriteButton.Label = _restaurantDB.Bookmarked ? LocalizedStrings.Get("RestaurantDetailPage_Favorited") : LocalizedStrings.Get("RestaurantDetailPage_ToFavorite");
        }

        private void SetupPivotItemHeader(bool detail, bool comment)
        {
            this.DetailPivotItemHeader.Foreground = detail ? new SolidColorBrush(Color.FromArgb(255, 224, 92, 82)) : new SolidColorBrush(Colors.Black);
            this.DetailPivotItemHeader.FontWeight = detail ? FontWeights.Bold : FontWeights.Normal;
            this.DetailPivotItemHeader.FontSize = detail ? 22 : 20;
            this.CommentPivotItemHeader.Foreground = comment ? new SolidColorBrush(Color.FromArgb(255, 224, 92, 82)) : new SolidColorBrush(Colors.Black);
            this.CommentPivotItemHeader.FontWeight = comment ? FontWeights.Bold : FontWeights.Normal;
            this.CommentPivotItemHeader.FontSize = comment ? 22 : 20;
        }

        private async void DownloadRestaurantCommentsAtPage(int page)
        {
            LoadingRing.IsActive = true;
            LoadingRing.Visibility = Visibility.Visible;
            LoadMoreButoon.Visibility = Visibility.Collapsed;

            var client = new HttpClient();
            string url = "http://www.vivelevendredi.com/_restaurants/json/rating-list/" + _restaurant.pk + "/?page=" + page;
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            LoadingRing.IsActive = false;
            LoadingRing.Visibility = Visibility.Collapsed;
            LoadMoreButoon.Visibility = Visibility.Visible;
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
            {
                return;
            }

            RestaurantComment _restaurantComment = Newtonsoft.Json.JsonConvert.DeserializeObject<RestaurantComment>(result);
            if (_restaurantComment.rating_list.Count < 12)
            {
                LoadMoreButoon.Visibility = Visibility.Collapsed;
            }
            if (_restaurantComment.rating_list.Count == 0 && _currentPage == 1)
            {
                this.NoCommentText.Visibility = Visibility.Visible;
            }
            else
            {
                this.NoCommentText.Visibility = Visibility.Collapsed;
            }
            foreach (LatestRating comment in _restaurantComment.rating_list)
            {
                comment.convertDateToChinese();
                comment.username = comment.user.username;
            }

            foreach (LatestRating comment in _restaurantComment.rating_list)
            {
                _comments.Comments.Add(comment);
                ImageDownloader.DownloadImageIntoImage(comment.user);
            }
            RestaurantCommentList.DataContext = _comments;
        }


        private void SetupRestaurantReview(Restaurant _restaurant)
        {
            BitmapImage halfStar = new BitmapImage();
            halfStar.UriSource = new Uri(this.Star1.BaseUri, "Assets/star_half.png");
            BitmapImage emptyStar = new BitmapImage();
            emptyStar.UriSource = new Uri(this.Star1.BaseUri, "Assets/star_empty.png");
            BitmapImage star = new BitmapImage();
            star.UriSource = new Uri(this.Star1.BaseUri, "Assets/star_full.png");
            double ratingScore = Double.Parse(_restaurant.rating_score);

            if (ratingScore == 0)
            {
                this.Star1.Source = emptyStar;
                this.Star2.Source = emptyStar;
                this.Star3.Source = emptyStar;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore > 0 && ratingScore < 1)
            {
                this.Star1.Source = halfStar;
                this.Star2.Source = emptyStar;
                this.Star3.Source = emptyStar;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore == 1)
            {
                this.Star1.Source = star;
                this.Star2.Source = emptyStar;
                this.Star3.Source = emptyStar;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore > 1 && ratingScore < 2)
            {
                this.Star1.Source = star;
                this.Star2.Source = halfStar;
                this.Star3.Source = emptyStar;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore == 2)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = emptyStar;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore > 2 && ratingScore < 3)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = halfStar;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore == 3)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = star;
                this.Star4.Source = emptyStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore > 3 && ratingScore < 4)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = star;
                this.Star4.Source = halfStar;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore == 4)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = star;
                this.Star4.Source = star;
                this.Star5.Source = emptyStar;
            }
            if (ratingScore > 4 && ratingScore < 5)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = star;
                this.Star4.Source = star;
                this.Star5.Source = halfStar;
            }
            if (ratingScore == 5)
            {
                this.Star1.Source = star;
                this.Star2.Source = star;
                this.Star3.Source = star;
                this.Star4.Source = star;
                this.Star5.Source = star;
            }
        }
    }
}
