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
            this.restaurantName.Text = restaurant.name;
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
            this.restaurantCommentNumber.Text = "" + restaurant.rating_num + "个点评";
            this.restaurantPhoneNumber1.Text = restaurant.phone_number_1;
            this.restaurantPhoneNumber2.Text = restaurant.phone_number_2;
            if (restaurant.latest_rating != null)
            {
                this.userName.Text = restaurant.latest_rating.user.username;
                this.userCommentDate.Text = restaurant.latest_rating.rate_date.Substring(0, 4) + "年" + restaurant.latest_rating.rate_date.Substring(5, 2) + "月" + restaurant.latest_rating.rate_date.Substring(8, 2) + "日";
                this.userComment.Text = restaurant.latest_rating.comment;
                this.userCommentScore.Text = "" + restaurant.latest_rating.score;
            }

        }

        private void Comment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantCommentPage), restaurant.pk);
        }
    }
}
