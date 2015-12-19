using Windows.ApplicationModel.Resources;

namespace Paris_Saveur.Tools
{
    class LocalizedStrings
    {
        public static string Get(string key)
        {
            return ResourceLoader.GetForCurrentView().GetString(key);
        }
    }
}
