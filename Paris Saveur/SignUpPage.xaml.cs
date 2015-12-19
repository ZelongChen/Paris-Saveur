using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;


namespace Paris_Saveur
{

    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
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

        private async void ValidationCheck()
        {

            //*********Null Input Check***********
            if (this.UserNameTextBox.Text == null || this.UserNameTextBox.Text == "")
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_UsernameEmptyText"));
                return;
            }
            else if (this.UserEmailTextBox.Text == null || this.UserEmailTextBox.Text == "")
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_EmailEmptyText"));
                return;
            }
            else if (this.Password1TextBox.Password == null || this.Password1TextBox.Password == "")
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_PasswordEmptyText"));
                return;
            }
            else if (this.Password2TextBox.Password == null || this.Password2TextBox.Password == "")
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_PasswordEmptyText"));
                return;
            }

            //*********User Name Length Check***********
            if (this.UserNameTextBox.Text.Length <= 3)
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_UsernameShortText"));
                return;
            }

            //*********Email Format Validation Check***********

            if (!IsValidEmail(this.UserEmailTextBox.Text))
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_EmailFormatText"));
                return;
            }

            //*********Two Password Identity Check***********
            if (Password1TextBox.Password != Password2TextBox.Password)
            {
                ShowErrorMessage(LocalizedStrings.Get("SignUpPage_TwoPwdNotMatchText"));
                return;
            }

            SendSignUpInformation();

        }

        private async void ShowErrorMessage(string content)
        {
            var dialogBuilder = new MessageDialog("");
            dialogBuilder.Title = LocalizedStrings.Get("Error");
            dialogBuilder.Content = content;
            var dialog = await dialogBuilder.ShowAsync();
        }

        private async void SendSignUpInformation()
        {
            this.LoadingRing.IsActive = true;
            this.LoadingRing.Visibility = Visibility.Visible;

            HttpFormUrlEncodedContent formContent = SetupHttpFormUrlEncodedContent();
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://www.vivelevendredi.com/accounts/signup/mobile/");
            HttpResponseMessage response = await client.PostAsync(uri, formContent);
            if (response.IsSuccessStatusCode)
            {
                SaveUserInformation(response);
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                HandleSignUpError(response);
            }

            this.LoadingRing.IsActive = false;
            this.LoadingRing.Visibility = Visibility.Collapsed;
        }

        private HttpFormUrlEncodedContent SetupHttpFormUrlEncodedContent()
        {
            string username = this.UserNameTextBox.Text;
            string email = this.UserEmailTextBox.Text;
            string password1 = this.Password1TextBox.Password;
            string password2 = this.Password2TextBox.Password;
            Dictionary<string, string> fullHttpContentDictionary = new Dictionary<string, string>();
            Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            string firmwareVersion = deviceInfo.SystemFirmwareVersion.ToString();
            fullHttpContentDictionary.Add("username", username);
            fullHttpContentDictionary.Add("email", email);
            fullHttpContentDictionary.Add("password1", password1);
            fullHttpContentDictionary.Add("password2", password2);
            return new HttpFormUrlEncodedContent(fullHttpContentDictionary);
        }

        private async void HandleSignUpError(HttpResponseMessage response) {
            string responseString = await response.Content.ReadAsStringAsync();
            JsonObject responseJson = JsonObject.Parse(responseString);
            string errorReason = responseJson.GetNamedArray("errors").GetStringAt(0);

            var dialogBuilder = new MessageDialog("");
            dialogBuilder.Title = LocalizedStrings.Get("Error");
            if (errorReason.Contains("username"))
            {
                dialogBuilder.Content = LocalizedStrings.Get("SignUpPage_UsernameExistText");
            }
            else if (errorReason.Contains("email"))
            {
                dialogBuilder.Content = LocalizedStrings.Get("SignUpPage_EmailExistText");
            }
            var dialog = dialogBuilder.ShowAsync();
        }

        private async void SaveUserInformation(HttpResponseMessage response)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["UserName"] = this.UserNameTextBox.Text;
            string responseString = await response.Content.ReadAsStringAsync();
            JsonObject responseJson = JsonObject.Parse(responseString);
            string token = responseJson.GetNamedObject("mobile_access").GetNamedString("token");
            localSettings.Values["AuthToken"] = token;
            string thumbnailUrl = responseJson.GetNamedObject("mobile_access").GetNamedObject("user").GetNamedString("avatar_url");
            localSettings.Values["ThumbnailUrl"] = thumbnailUrl;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = null;
			pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(email, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
