using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.DataBase
{
    class RestaurantDB
    {

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public int pk { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public string style { get; set; }
        public string address { get; set; }
        public double geo_lat { get; set; }
        public double geo_lon { get; set; }
        public string public_transit { get; set; }
        public string phone_number_1 { get; set; }
        public string phone_number_2 { get; set; }
        public string opening_hours { get; set; }
        public string website { get; set; }
        public bool is_closed { get; set; }
        public bool is_locked { get; set; }
        public string url_path { get; set; }
        public int rating_num { get; set; }
        public string rating_score { get; set; }
        public string RatingScoreAndReviewNum { get; set; }
        public int consumption_num { get; set; }
        public string consumption_per_capita { get; set; }
        public string ViewTime { get; set; }
        public bool Bookmarked { get; set; }

        public RestaurantDB() { }

        public void SetupRestaurantDB(Restaurant restaurant) {
            this.pk = restaurant.pk;
            this.name = restaurant.name;
            this.description = restaurant.description;
            this.style = restaurant.style;
            this.thumbnail = restaurant.thumbnail;
            this.address = restaurant.address;
            this.geo_lat = restaurant.geo_lat;
            this.geo_lon = restaurant.geo_lon;
            this.public_transit = restaurant.public_transit;
            this.phone_number_1 = restaurant.phone_number_1;
            this.phone_number_2 = restaurant.phone_number_2;
            this.opening_hours = restaurant.opening_hours;
            this.website = restaurant.website;
            this.url_path = restaurant.url_path;
            this.rating_num = restaurant.rating_num;
            this.rating_score = restaurant.rating_score;
            this.RatingScoreAndReviewNum = restaurant.RatingScoreAndReviewNum;
            this.consumption_num = restaurant.consumption_num;
            this.consumption_per_capita = restaurant.consumption_per_capita;
        }
    }
}
