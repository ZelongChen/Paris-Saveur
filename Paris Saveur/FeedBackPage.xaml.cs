using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
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
            fullHttpContentDictionary.Add("device_name", "WindowsPhone");
            Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            string firmwareVersion = deviceInfo.SystemFirmwareVersion.ToString();
            fullHttpContentDictionary.Add("device_info", firmwareVersion);
            fullHttpContentDictionary.Add("app_name", "v0.8WP");
            fullHttpContentDictionary.Add("app_version", "1.0");
            fullHttpContentDictionary.Add("comment", feedbackContent);
            HttpFormUrlEncodedContent formContent = new HttpFormUrlEncodedContent(fullHttpContentDictionary);
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://www.vivelevendredi.com/feedbacks/submit/mobile/");
            HttpResponseMessage response = await client.PostAsync(uri, formContent);
            if (response.IsSuccessStatusCode)
            {
                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
                XmlNodeList elements = toastXml.GetElementsByTagName("text");
                elements[0].AppendChild(toastXml.CreateTextNode("感谢您的反馈"));
                ToastNotification toast = new ToastNotification(toastXml);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
                this.FeedBackTextBox.Text = "";
            }
            this.LoadingRing.IsActive = false;
            this.LoadingRing.Visibility = Visibility.Collapsed;
        }
    }
}
