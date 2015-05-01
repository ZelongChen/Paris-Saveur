using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Tools
{
    class ConnectionContext
    {
        public static bool checkNetworkConnection()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        public static bool isUserSignedIn()
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
    }
}
