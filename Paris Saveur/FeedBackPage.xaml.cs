using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.Web.Http;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace Paris_Saveur
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FeedBackPage : Page
    {
        public FeedBackPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        async void SendFeedBackButton_Click(object sender, RoutedEventArgs e)
        {
            this.LoadingRing.IsActive = true;
            this.LoadingRing.Visibility = Visibility.Visible;

            String feedbackContent = this.FeedBackTextBox.Text;
            Dictionary<string, string> fullHttpContentDictionary = new Dictionary<string,string>();
            fullHttpContentDictionary.Add("device_name", "Android");
            fullHttpContentDictionary.Add("device_info", "3.0.8-02825-g9e39b8c+%28REL%2C+API+level+15%29+device%3AD01E%2C+model%3AKFTT%2C+product%3AKindle+Fire");
            fullHttpContentDictionary.Add("app_name", "v5.android");
            fullHttpContentDictionary.Add("app_version", "1.0");
            fullHttpContentDictionary.Add("comment", feedbackContent);
            HttpFormUrlEncodedContent formContent = new HttpFormUrlEncodedContent(fullHttpContentDictionary);
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://www.vivelevendredi.com/feedbacks/submit/mobile/");
            HttpResponseMessage response = await client.PostAsync(uri, formContent);
            string responseBody = await response.Content.ReadAsStringAsync();

            this.LoadingRing.IsActive = false;
            this.LoadingRing.Visibility = Visibility.Collapsed;
        }
    }
}
