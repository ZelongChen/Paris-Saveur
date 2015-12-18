using System;
using System.Collections.Generic;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;


namespace Paris_Saveur
{

    public sealed partial class CommentPage : Page
    {
        BitmapImage _emptyStarBitmap;
        BitmapImage _fullStarBitmap;
        int _score;
        int _restaurantPK;

        public CommentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _emptyStarBitmap = new BitmapImage(new Uri(this.BaseUri, "Assets/star_empty.png"));
            _fullStarBitmap = new BitmapImage(new Uri(this.BaseUri, "Assets/star_full.png"));
            _score = 0;
            _restaurantPK = Int32.Parse(e.Parameter.ToString());
        }

        private void StarImage1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChooseStars(true, false, false, false, false);
            this.AttitudeText.Text = "糟糕";
            _score = 1;
        }

        private void StarImage2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChooseStars(true, true, false, false, false);
            this.AttitudeText.Text = "较差";
            _score = 2;
        }

        private void StarImage3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChooseStars(true, true, true, false, false);
            this.AttitudeText.Text = "一般";
            _score = 3;
        }

        private void StarImage4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChooseStars(true, true, true, true, false);
            this.AttitudeText.Text = "推荐";
            _score = 4;
        }

        private void StarImage5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChooseStars(true, true, true, true, true);
            this.AttitudeText.Text = "力荐";
            _score = 5;
        }

        private async void SendCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_score == 0)
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
                Uri uri = new Uri("http://www.vivelevendredi.com/restaurants/rate/mobile/" + _restaurantPK + "/");
                HttpResponseMessage response = await client.PostAsync(uri, formContent);

                if (response.IsSuccessStatusCode)
                {
                    Frame.GoBack();
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
            fullHttpContentDictionary.Add("comment", comment);
            fullHttpContentDictionary.Add("price", price);
            fullHttpContentDictionary.Add("score", "" + _score);
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string authToken = (String)localSettings.Values["AuthToken"];
            string username = (String)localSettings.Values["UserName"];
            fullHttpContentDictionary.Add("mobile_access_token", authToken);
            fullHttpContentDictionary.Add("username", username);
            return new HttpFormUrlEncodedContent(fullHttpContentDictionary);
        }

        private void ChooseStars(bool first, bool second, bool third, bool fourth, bool fifth)
        {
            this.StarImage1.Source = first ? _fullStarBitmap : _emptyStarBitmap;
            this.StarImage2.Source = second ? _fullStarBitmap : _emptyStarBitmap;
            this.StarImage3.Source = third ? _fullStarBitmap : _emptyStarBitmap;
            this.StarImage4.Source = fourth ? _fullStarBitmap : _emptyStarBitmap;
            this.StarImage5.Source = fifth ? _fullStarBitmap : _emptyStarBitmap;
        }
    }
}
