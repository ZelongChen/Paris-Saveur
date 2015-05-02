using Newtonsoft.Json;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;


namespace Paris_Saveur
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionContext.CheckNetworkConnection())
            {
                ValidationCheck();
            }
            else
            {
                ConnectionContext.ShowNoConnectionWarning();
            }

        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));
        }

        private async void ValidationCheck()
        {
            //*********Null Input Check***********
            var dialogBuilder = new MessageDialog("");
            dialogBuilder.Title = "错误";
            if (this.UserNameTextBox.Text == null || this.UserNameTextBox.Text == "")
            {
                dialogBuilder.Content = "用户名不能为空";
                var dialog = await dialogBuilder.ShowAsync();
                return;
            }

            else if (this.PasswordTextBox.Password == null || this.PasswordTextBox.Password == "")
            {
                dialogBuilder.Content = "密码不能为空";
                var dialog = await dialogBuilder.ShowAsync();
                return;
            }

            //*********User Name Length Check***********
            if (this.UserNameTextBox.Text.Length <= 3)
            {
                dialogBuilder.Content = "用户名不能少于4个字符";
                var dialog = await dialogBuilder.ShowAsync();
                return;
            }

            SendLoginInformation();
        }

        private async void SendLoginInformation()
        {
            this.LoadingRing.IsActive = true;
            this.LoadingRing.Visibility = Visibility.Visible;

            HttpFormUrlEncodedContent formContent = SetupHttpFormUrlEncodedContent();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://www.vivelevendredi.com/accounts/login/mobile/");
            HttpResponseMessage response = await client.PostAsync(uri, formContent);
            if (response.IsSuccessStatusCode)
            {
                SaveUserInformation(response);
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                HandleLoginError(response);
            }

            this.LoadingRing.IsActive = false;
            this.LoadingRing.Visibility = Visibility.Collapsed;
        }

        private HttpFormUrlEncodedContent SetupHttpFormUrlEncodedContent()
        {
            string username = this.UserNameTextBox.Text;
            string password = this.PasswordTextBox.Password;
            Dictionary<string, string> fullHttpContentDictionary = new Dictionary<string, string>();
            Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            string firmwareVersion = deviceInfo.SystemFirmwareVersion.ToString();
            fullHttpContentDictionary.Add("username", username);
            fullHttpContentDictionary.Add("password", password);
            return new HttpFormUrlEncodedContent(fullHttpContentDictionary);
        }

        private async void SaveUserInformation(HttpResponseMessage response)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["UserName"] = this.UserNameTextBox.Text;
            string responseString = await response.Content.ReadAsStringAsync();
            JsonObject responseJson =  JsonObject.Parse(responseString);
            string token = responseJson.GetNamedObject("mobile_access").GetNamedString("token");
            localSettings.Values["AuthToken"] = token;
            string thumbnailUrl = responseJson.GetNamedObject("mobile_access").GetNamedObject("user").GetNamedString("avatar_url");
            localSettings.Values["ThumbnailUrl"] = thumbnailUrl;
        }

        private async void HandleLoginError(HttpResponseMessage response)
        {
            string responseString = await response.Content.ReadAsStringAsync();
            JsonObject responseJson = JsonObject.Parse(responseString);
            string errorReason = responseJson.GetNamedArray("errors").GetStringAt(0);

            var dialogBuilder = new MessageDialog(errorReason);
            dialogBuilder.Title = "错误";
            var dialog = dialogBuilder.ShowAsync();
        }
    }
}
