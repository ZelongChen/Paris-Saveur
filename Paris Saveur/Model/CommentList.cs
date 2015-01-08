using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paris_Saveur.Model
{
    class CommentList : INotifyPropertyChanged
    {
        private List<LatestRating> comments = new List<LatestRating>();
        public List<LatestRating> Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                NotifyPropertyChanged("comments");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }   
    }
}
