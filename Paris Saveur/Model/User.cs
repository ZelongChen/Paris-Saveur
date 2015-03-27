using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Paris_Saveur.Model
{
    class User : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string username { get; set; }
        public string display_name { get; set; }
        public string avatar_url { get; set; }
        private BitmapImage avatarThumbnailBitmap;
        public BitmapImage AvatarThumbnailBitmap
        {
            get { return avatarThumbnailBitmap; }
            set
            {
                avatarThumbnailBitmap = value;
                NotifyPropertyChanged("AvatarThumbnailBitmap");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
