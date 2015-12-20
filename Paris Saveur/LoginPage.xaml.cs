using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;


namespace Paris_Saveur
{
    public sealed partial class LoginPage : Page
    {
        private bool _goBackToDetailPage;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() == "ComeFromDetailPage")
            {
                _goBackToDetailPage = true;
            }
            else
            {
                _goBackToDetailPage = false;
            }
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
            if (this.UserNameTextBox.Text == null || this.UserNameTextBox.Text == "")
            {
                ShowMessageDialog(LocalizedStrings.Get("Error"), LocalizedStrings.Get("LoginPage_UserNameEmpty"));
                return;
            }

            else if (this.PasswordTextBox.Password == null || this.PasswordTextBox.Password == "")
            {
                ShowMessageDialog(LocalizedStrings.Get("Error"), LocalizedStrings.Get("LoginPage_PasswordEmpty"));
                return;
            }

            //*********User Name Length Check***********
            if (this.UserNameTextBox.Text.Length <= 3)
            {
                ShowMessageDialog(LocalizedStrings.Get("Error"), LocalizedStrings.Get("LoginPage_UserNameShort"));
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
                if (_goBackToDetailPage)
                {
                    Frame.GoBack();
                }
                else
                {
                    Frame.Navigate(typeof(MainPage));
                }
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
            ShowMessageDialog(LocalizedStrings.Get("Error"), errorReason);
        }

        private void ShowMessageDialog(string title, string content)
        {
            var dialogBuilder = new MessageDialog(content);
            dialogBuilder.Title = title;
            var dialog = dialogBuilder.ShowAsync();
        }
    }
}
