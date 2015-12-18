using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur
{

    public sealed partial class RestaurantSortByStylePage : Page
    {
        public RestaurantSortByStylePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Sichuan_Hunan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Sichuan_Hunan");
        }

        private void Shandong_Anhui_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Shandong_Anhui");
        }

        private void Jiangshu_Zhejiang_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Jiangsu_Zhejiang");
        }

        private void Guangdong_Fujian_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Cantonese_Fujian");
        }

        private void Yunnan_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Yunnan");
        }

        private void Beifang_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Northern_Chinese");
        }

        private void Japan_Korea_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Japanese_Korean");
        }

        private void Southest_Asia_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "South_Asian");
        }

        private void Unclassified_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(HotRestaurantPage), "Unclassified");
        }
    }
}
