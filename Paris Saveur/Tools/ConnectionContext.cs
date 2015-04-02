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
    }
}
