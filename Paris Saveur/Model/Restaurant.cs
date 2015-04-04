using Paris_Saveur.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Paris_Saveur
{
    class Restaurant : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

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
        public List<String> tag_list { get; set; }
        public LatestRating latest_rating { get; set; }
        private BitmapImage thumbnailBitmap;
       
        public BitmapImage ThumbnailBitmap 
        {
            get { return thumbnailBitmap; }
            set
            {
                thumbnailBitmap = value;
                NotifyPropertyChanged("ThumbnailBitmap");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void ConvertRestaurantStyleToChinese()
        {
            if (this.style.Equals("Shandong_Anhui"))
            {
                this.style = "鲁菜 徽菜";
            }
            else if (this.style.Equals("Sichuan_Hunan"))
            {
                this.style = "川菜 湘菜";
            }
            else if (this.style.Equals("Japanese_Korean"))
            {
                this.style = "日餐 韩餐";
            }
            else if (this.style.Equals("Northern_Chinese"))
            {
                this.style = "北方菜系";
            }
            else if (this.style.Equals("Cantonese_Fujian"))
            {
                this.style = "粤菜 闽菜";
            }
            else
            {
                this.style = "未归类";
            }

        }

        public void ShowReviewScoreAndNumber()
        {
            this.RatingScoreAndReviewNum = this.rating_score + " (" + rating_num + "个点评)";

        }

        public void ShowPrice()
        {
            this.consumption_per_capita = "人均" + this.consumption_per_capita + "€";
        }
    }
}
