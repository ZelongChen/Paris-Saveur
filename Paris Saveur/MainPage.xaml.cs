using Paris_Saveur.DataBase;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.System;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace Paris_Saveur
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string appbarTileId = "ParisSaveurSecondaryTile.AppBar";

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void recommendedText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecommendedPage));
        }

        private void hotText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage));
        }

        private void nearbyText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(NearByRestaurant));
        }

        private void sortbyText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantSortByStylePage));
        }

        private void hottagText_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotTagPage));
        }

        private void metroText_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Mainpage_Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MainPagePivot.SelectedIndex == 0)
            {
                this.HistoryButton.Visibility = Visibility.Visible;
                this.FavoriteButton.Visibility = Visibility.Visible;
                this.SearchButton.Visibility = Visibility.Visible;
                this.FeedbackButton.Visibility = Visibility.Collapsed;
                this.WebButton.Visibility = Visibility.Collapsed;
                this.NoteButton.Visibility = Visibility.Collapsed;

                this.PivotItem0_Title.Foreground = new SolidColorBrush(Color.FromArgb(100, 224, 92, 82));
                this.PivotItem0_Title.FontWeight = FontWeights.Bold;
                this.PivotItem0_Title.FontSize = 22;
                this.PivotItem1_Title.Foreground = new SolidColorBrush(Colors.Black);
                this.PivotItem1_Title.FontWeight = FontWeights.Normal;
                this.PivotItem1_Title.FontSize = 20;
            }
            else
            {
                this.HistoryButton.Visibility = Visibility.Collapsed;
                this.FavoriteButton.Visibility = Visibility.Collapsed;
                this.SearchButton.Visibility = Visibility.Collapsed;
                this.FeedbackButton.Visibility = Visibility.Visible;
                this.WebButton.Visibility = Visibility.Visible;
                this.NoteButton.Visibility = Visibility.Visible;

                this.PivotItem0_Title.Foreground = new SolidColorBrush(Colors.Black);
                this.PivotItem0_Title.FontWeight = FontWeights.Normal;
                this.PivotItem0_Title.FontSize = 20;
                this.PivotItem1_Title.Foreground = new SolidColorBrush(Color.FromArgb(100, 224, 92, 82));
                this.PivotItem1_Title.FontWeight = FontWeights.Bold;
                this.PivotItem1_Title.FontSize = 22;
            }
        }

        async void Launch_Web_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.newsavour.com/restaurants/"));
        }

        private void Send_Feedback_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FeedBackPage));
        }

        async private void Follow_Weibo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://www.weibo.com/vivelevendredi"));
        }


        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string uriToLaunch = @"ms-windows-store:reviewapp?appid=c59700e8-429d-4c97-a442-624dd6a127c9";
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
    }
}
