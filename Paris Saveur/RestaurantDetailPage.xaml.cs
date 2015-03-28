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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Paris_Saveur
{

    public sealed partial class RestaurantDetailPage : Page
    {
        public RestaurantDetailPage()
        {
            this.InitializeComponent();
            SocialNetworks = new List<string>
            {
                "Facebook",
                "Twitter",
                "微博",
                "微信",
                "人人网"
            };
        }

        Restaurant restaurant;
        private List<String> SocialNetworks { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            restaurant = e.Parameter as Restaurant;
            this.PageTitle.Text = restaurant.name;
            this.restaurantThumbnail.Source = restaurant.ThumbnailBitmap;
            this.restaurantStyle.Text = restaurant.style;
            this.restaurantPrice.Text = restaurant.consumption_per_capita;
            SetupRestaurantReview(restaurant);
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
            if (restaurant.phone_number_2 != "")
            {
                this.restaurantPhoneNumber2.Text = restaurant.phone_number_2;
                this.restaurantPhoneNumber2.Visibility = Visibility.Visible;
            }
            else
            {
                this.restaurantPhoneNumber2.Visibility = Visibility.Collapsed;
            }
            
            this.sharePage.ItemsSource = SocialNetworks;
        }

        private void SetupRestaurantReview(Restaurant restaurant)
        {
            BitmapImage halfStar = new BitmapImage();
            halfStar.UriSource = new Uri(this.star1.BaseUri, "Assets/star_half.png");
            BitmapImage emptyStar = new BitmapImage();
            emptyStar.UriSource = new Uri(this.star1.BaseUri, "Assets/star_empty.png");
            BitmapImage star = new BitmapImage();
            star.UriSource = new Uri(this.star1.BaseUri, "Assets/star_full.png");
            double ratingScore = Double.Parse(restaurant.rating_score);
            
            if (ratingScore == 0)
            {
                this.star1.Source = emptyStar;
                this.star2.Source = emptyStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 0 && ratingScore <1)
            {
                this.star1.Source = halfStar;
                this.star2.Source = emptyStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 1)
            {
                this.star1.Source = star;
                this.star2.Source = emptyStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore >1 && ratingScore < 2)
            {
                this.star1.Source = star;
                this.star2.Source = halfStar;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 2)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = emptyStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 2 && ratingScore < 3)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = halfStar;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 3)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = emptyStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 3 && ratingScore < 4)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = halfStar;
                this.star5.Source = emptyStar;
            }
            if (ratingScore == 4)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = star;
                this.star5.Source = emptyStar;
            }
            if (ratingScore > 4 && ratingScore < 5)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = star;
                this.star5.Source = halfStar;
            }
            if (ratingScore == 5)
            {
                this.star1.Source = star;
                this.star2.Source = star;
                this.star3.Source = star;
                this.star4.Source = star;
                this.star5.Source = star;
            }
        }

        private void Comment_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RestaurantCommentPage), restaurant.pk);
        }
    }
}
