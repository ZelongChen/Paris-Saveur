using Paris_Saveur.Tools;
using System;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Paris_Saveur
{

    public sealed partial class MainPage : Page
    {
        public const string APP_BAR_TILE_ID = "ParisSaveurSecondaryTile.AppBar";
        private const string HOME_PAGE_URL = "http://www.newsavour.com/restaurants/";
        private const string WEIBO_URL = "http://www.weibo.com/vivelevendredi";
        private const string STORE_URL = @"ms-windows-store:reviewapp?appid=03a26f4a-b9b9-4ed0-8bd6-e28979de5884";

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UpdateLoginPage();
        }

        private void RecommendedText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecommendedPage));
        }

        private void HotText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage));
        }

        private void NearByText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NearByRestaurant));
        }

        private void SortByText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantSortByStylePage));
        }

        private void HotTagText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotTagPage));
        }

        private void MetroText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(TransportStationsPage));
        }

        private void Mainpage_Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MainPagePivot.SelectedIndex == 0)
            {
                SwitchBetweenPivotItem(true, false, false);
            }
            else if (this.MainPagePivot.SelectedIndex == 1)
            {
                SwitchBetweenPivotItem(false, true, false);
            }
            else
            {
                SwitchBetweenPivotItem(false, false, true);
                UpdateLoginPage();

            }
        }

        async void Launch_Web_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(HOME_PAGE_URL));
        }

        private void Send_Feedback_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FeedBackPage));
        }

        async private void Follow_Weibo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(WEIBO_URL));
        }


        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = STORE_URL;
            var uri = new Uri(uriToLaunch);
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SimpleDisplayPage), "history");
        }

        private void Bookmark_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SimpleDisplayPage), "favorite");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ResourceLoader();
            if (ConnectionContext.IsUserSignedIn())
            {
                var dialogBuilder = new MessageDialog(loader.GetString("MainPage_LogoutMessageDialogHeader"));
                dialogBuilder.Title = loader.GetString("MainPage_LogoutMessageDialogTitle");
                dialogBuilder.Commands.Add(new UICommand { Label = loader.GetString("Yes"), Id = 0 });
                dialogBuilder.Commands.Add(new UICommand { Label = loader.GetString("Cancel"), Id = 1 });
                var dialog = await dialogBuilder.ShowAsync();

                if ((int)dialog.Id == 0)
                {
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    localSettings.Values.Remove("AuthToken");
                    localSettings.Values.Remove("UserName");
                    UpdateLoginPage();
                }
            }
            else
            {
                if (ConnectionContext.CheckNetworkConnection())
                {
                    Frame.Navigate(typeof(LoginPage));
                }
                else
                {
                    ConnectionContext.ShowNoConnectionWarning();
                }
                
            }
        }

        private void SwitchBetweenPivotItem(bool first, bool second, bool third)
        {
            this.SearchButton.Visibility = first ? Visibility.Visible : Visibility.Collapsed;
            this.FeedbackButton.Visibility = second ? Visibility.Visible : Visibility.Collapsed;
            this.WebButton.Visibility = second ? Visibility.Visible : Visibility.Collapsed;
            this.NoteButton.Visibility = second ? Visibility.Visible : Visibility.Collapsed;
            this.HistoryButton.Visibility = third ? Visibility.Visible : Visibility.Collapsed;
            this.FavoriteButton.Visibility = third ? Visibility.Visible : Visibility.Collapsed;

            this.PivotItem0_Title.Foreground = first ? new SolidColorBrush(Color.FromArgb(255, 224, 92, 82)) : new SolidColorBrush(Colors.Black);
            this.PivotItem0_Title.FontWeight = first ? FontWeights.Bold : FontWeights.Normal;
            this.PivotItem0_Title.FontSize = first ? 22 : 20;

            this.PivotItem1_Title.Foreground = second ? new SolidColorBrush(Color.FromArgb(255, 224, 92, 82)) : new SolidColorBrush(Colors.Black);
            this.PivotItem1_Title.FontWeight = second ? FontWeights.Bold : FontWeights.Normal;
            this.PivotItem1_Title.FontSize = second ? 22 : 20;

            this.PivotItem2_Title.Foreground = third ? new SolidColorBrush(Color.FromArgb(255, 224, 92, 82)) : new SolidColorBrush(Colors.Black);
            this.PivotItem2_Title.FontWeight = third ? FontWeights.Bold : FontWeights.Normal;
            this.PivotItem2_Title.FontSize = third ? 22 : 20;
        }

        private void UpdateLoginPage()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var loader = new ResourceLoader();
            if (ConnectionContext.IsUserSignedIn())
            {
                this.LoginButton.Background = new SolidColorBrush(Colors.Red);
                this.LoginButton.Content = loader.GetString("MainPage_PivotItemMeLogin"); ;
                this.UserNameText.Text = (String)localSettings.Values["UserName"];
                string thumbnailUrl = (String)localSettings.Values["ThumbnailUrl"];
                ImageDownloader.DownloadImageIntoImage(this.UserThumbnailImageView, thumbnailUrl);
            }
            else
            {
                this.LoginButton.Background = new SolidColorBrush(Colors.Green);
                this.LoginButton.Content = loader.GetString("MainPage_PivotItemMeLogin");
                this.UserNameText.Text = "";
                BitmapImage placeholder = new BitmapImage(new Uri(this.BaseUri, "Assets/annonymous.jpg"));
                this.UserThumbnailImageView.Source = placeholder;
            }
        }
    }
}
