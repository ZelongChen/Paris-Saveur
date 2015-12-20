﻿using Paris_Saveur.DataBase;
using Paris_Saveur.Model;
using Paris_Saveur.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
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

        public void SetupRestaurantFromDB(RestaurantDB restaurant)
        {
            this.pk = restaurant.Id;
            this.name = restaurant.name;
            this.description = restaurant.description;
            this.thumbnail = restaurant.thumbnail;
            this.style = restaurant.style;
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

        /*private async void GetImageFromFile(RestaurantDB restaurantDB)
        {
            //open the picture library
            StorageFolder libfolder = KnownFolders.PicturesLibrary;
            //get all folders first
            IReadOnlyList<StorageFolder> folderList = await libfolder.GetFoldersAsync();
            //select our app's folder
            var appfolder = folderList.FirstOrDefault(f => f.Name.Contains("ParisSaveurImageFolder"));
            //get the desired file (assuming you know the file name)
            StorageFile picfile = await appfolder.GetFileAsync(restaurantDB.name + ".jpg");
            //generate a stream from the StorageFile
            var stream = await picfile.OpenAsync(FileAccessMode.Read);
            //generate a new image and set the source to our stream
            BitmapImage img = new BitmapImage();
            img.SetSource(stream);
            this.ThumbnailBitmap = img;
            //todo: work with the image
        }*/

        public void ConvertRestaurantStyleToChinese()
        {
            if (this.style.Equals("Shandong_Anhui"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Shandong_Anhui.Content");
            }
            else if (this.style.Equals("Sichuan_Hunan"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Sichuan_Hunan.Content");
            }
            else if (this.style.Equals("Japanese_Korean"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Japan_Korea.Content");
            }
            else if (this.style.Equals("Northern_Chinese"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Beifang.Content");
            }
            else if (this.style.Equals("Cantonese_Fujian"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Guangdong_Fujian.Content");
            }
            else if (this.style.Equals("Jiangsu_Zhejiang"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Jiangshu_Zhejiang.Content");
            }
            else if (this.style.Equals("South_Asian"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Southest_Asia.Content");
            }
            else if (this.style.Equals("Yunnan"))
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Yunnan.Content");
            }
            else
            {
                this.style = LocalizedStrings.Get("RestaurantSortByStylePage_Unclassified.Content");
            }
        }

        public static string StyleToChinese(string style)
        {
            switch (style)
            {
                case "Sichuan_Hunan":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Sichuan_Hunan.Content");
                case "Shandong_Anhui":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Shandong_Anhui.Content");
                case "Jiangsu_Zhejiang":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Jiangshu_Zhejiang.Content");
                case "Cantonese_Fujian":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Guangdong_Fujian.Content");
                case "Yunnan":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Yunnan.Content");
                case "Northern_Chinese":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Beifang.Content");
                case "Japanese_Korean":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Japan_Korea.Content");
                case "South_Asian":
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Southest_Asia.Content");
                default:
                    return LocalizedStrings.Get("RestaurantSortByStylePage_Unclassified.Content");
            }
        }

        public void ShowReviewScoreAndNumber()
        {
            this.RatingScoreAndReviewNum = this.rating_score + " (" + rating_num + LocalizedStrings.Get("RestaurantModel_CommentNumbers") +")";

        }

        public void ShowPrice()
        {
            this.consumption_per_capita = LocalizedStrings.Get("RestaurantModel_Per") + this.consumption_per_capita + "€";
        }
    }
}
