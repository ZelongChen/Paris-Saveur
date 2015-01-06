using Paris_Saveur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur
{
    class Restaurant
    {
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
        public double rating_score { get; set; }
        public int consumption_num { get; set; }
        public double consumption_per_capita { get; set; }
        //public List<String> dish_list { get; set; }
        public List<String> tag_list { get; set; }
        public LatestRating latest_rating { get; set; }
    }
}
