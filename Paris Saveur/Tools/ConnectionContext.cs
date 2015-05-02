using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Paris_Saveur.Tools
{
    class ConnectionContext
    {
        public static bool CheckNetworkConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        public static bool IsUserSignedIn()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["AuthToken"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ShowNoConnectionWarning()
        {
            var dialogBuilder = new MessageDialog("请检查您的网络连接");
            dialogBuilder.Title = "请注意";
            var dialog = dialogBuilder.ShowAsync();
        }
    }
}
