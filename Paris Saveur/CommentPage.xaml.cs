using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;


namespace Paris_Saveur
{

    public sealed partial class CommentPage : Page
    {
        public CommentPage()
        {
            this.InitializeComponent();
        }

        BitmapImage EmptyStarBitmap;
        BitmapImage FullStarBitmap;
        int Score = 0;
        int RestaurantPK;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            EmptyStarBitmap = new BitmapImage(new Uri(this.BaseUri, "Assets/star_empty.png"));
            FullStarBitmap = new BitmapImage(new Uri(this.BaseUri, "Assets/star_full.png"));
            RestaurantPK = Int32.Parse(e.Parameter.ToString());
        }

        private void StarImage1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.StarImage1.Source = FullStarBitmap;
            this.StarImage2.Source = EmptyStarBitmap;
            this.StarImage3.Source = EmptyStarBitmap;
            this.StarImage4.Source = EmptyStarBitmap;
            this.StarImage5.Source = EmptyStarBitmap;

            this.AttitudeText.Text = "糟糕";
            this.Score = 1;
        }

        private void StarImage2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.StarImage1.Source = FullStarBitmap;
            this.StarImage2.Source = FullStarBitmap;
            this.StarImage3.Source = EmptyStarBitmap;
            this.StarImage4.Source = EmptyStarBitmap;
            this.StarImage5.Source = EmptyStarBitmap;

            this.AttitudeText.Text = "较差";
            this.Score = 2;
        }

        private void StarImage3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.StarImage1.Source = FullStarBitmap;
            this.StarImage2.Source = FullStarBitmap;
            this.StarImage3.Source = FullStarBitmap;
            this.StarImage4.Source = EmptyStarBitmap;
            this.StarImage5.Source = EmptyStarBitmap;

            this.AttitudeText.Text = "一般";
            this.Score = 3;
        }

        private void StarImage4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.StarImage1.Source = FullStarBitmap;
            this.StarImage2.Source = FullStarBitmap;
            this.StarImage3.Source = FullStarBitmap;
            this.StarImage4.Source = FullStarBitmap;
            this.StarImage5.Source = EmptyStarBitmap;

            this.AttitudeText.Text = "推荐";
            this.Score = 4;
        }

        private void StarImage5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.StarImage1.Source = FullStarBitmap;
            this.StarImage2.Source = FullStarBitmap;
            this.StarImage3.Source = FullStarBitmap;
            this.StarImage4.Source = FullStarBitmap;
            this.StarImage5.Source = EmptyStarBitmap;

            this.AttitudeText.Text = "力荐";
            this.Score = 5;
        }

        private async void SendCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Score == 0)
            {
                var dialogBuilder = new MessageDialog("请轻按星标打分");
                dialogBuilder.Title = "请注意";
                var dialog = dialogBuilder.ShowAsync();
            }
            else
            {
                this.LoadingRing.IsActive = true;
                this.LoadingRing.Visibility = Visibility.Visible;

                HttpFormUrlEncodedContent formContent = SetupHttpFormUrlEncodedContent();
                HttpClient client = new HttpClient();
                Uri uri = new Uri("http://www.vivelevendredi.com/restaurants/rate/mobile/" + RestaurantPK + "/");
                HttpResponseMessage response = await client.PostAsync(uri, formContent);

                if (response.IsSuccessStatusCode)
                {
                    Frame.Navigate(typeof(RestaurantDetailPage));
                }
                else
                {
                    var dialogBuilder = new MessageDialog("评论不成功，请稍后再试");
                    dialogBuilder.Title = "请注意";
                    var dialog = dialogBuilder.ShowAsync();
                }

                this.LoadingRing.IsActive = false;
                this.LoadingRing.Visibility = Visibility.Collapsed;
            }
            
        }

        private HttpFormUrlEncodedContent SetupHttpFormUrlEncodedContent()
        {
            string comment = this.CommentTextBox.Text;
            string price = this.PriceTextBox.Text;
            Dictionary<string, string> fullHttpContentDictionary = new Dictionary<string, string>();
            Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            string firmwareVersion = deviceInfo.SystemFirmwareVersion.ToString();
            fullHttpContentDictionary.Add("username", comment);
            fullHttpContentDictionary.Add("password", price);
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string authToken = (String)localSettings.Values["AuthToken"];
            string username = (String)localSettings.Values["UserName"];
            fullHttpContentDictionary.Add("mobile_access_token", authToken);
            fullHttpContentDictionary.Add("username", username);
            return new HttpFormUrlEncodedContent(fullHttpContentDictionary);
        }
    }
}
