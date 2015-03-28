using Paris_Saveur.Tools;
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


namespace Paris_Saveur
{

    public sealed partial class RestaurantDetailPage : Page
    {
        public RestaurantDetailPage()
        {
            this.InitializeComponent();
        }

        Restaurant restaurant;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            restaurant = e.Parameter as Restaurant;
            this.PageTitle.Text = restaurant.name;
            this.restaurantThumbnail.Source = restaurant.ThumbnailBitmap;
            restaurant.ConvertRestaurantStyleToChinese();
            restaurant.ShowReviewScoreAndNumber();
            restaurant.ShowPrice();
            this.restaurantStyle.Text = restaurant.style;
            this.restaurantPrice.Text = restaurant.consumption_per_capita;
            this.restaurantReview.Text = restaurant.rating_score;
            if (restaurant.description != "")
            {
                this.restaurantDescription.Text = restaurant.description;
            }
            else
            {
                this.restaurantDescription.Visibility = Visibility.Collapsed;
            }
            this.restaurantAddress.Text = restaurant.address;
            this.restaurantMetro.Text = restaurant.public_transit;
            this.restaurantTime.Text = restaurant.opening_hours;
            this.restaurantPhoneNumber1.Text = restaurant.phone_number_1;
            this.restaurantPhoneNumber2.Text = restaurant.phone_number_2;

        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantCommentPage), restaurant.pk);
        }
    }
}
